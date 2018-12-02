﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Range(0, 1)]
    [Tooltip("The general volume all clips will be affected by")]
    public float volume = 1;
    [Tooltip("Leave as -1 to not play a song on start")]
    public int startingMusic = -1;

    private List<AudioClip[]> _songList = new List<AudioClip[]>();
    private List<AudioClip> _soundEffectList = new List<AudioClip>();

    private AudioSource _songSource;

    void Start()
    {
        /*
         * Initialize audio lists, probably with preloaded assets
         * 
        songs = PrefabRepository.Instance.Songs;
        */

        _soundEffectList = Resources.LoadAll<AudioClip>("Chat").ToList();

        _songSource = gameObject.AddComponent<AudioSource>();
        if (startingMusic >= 0)
            PlayMusic(startingMusic);
    }

    public void PlayAudio(int sound, float pitch = 1)
    {
        AudioSource effect = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        effect.pitch = pitch;
        effect.clip = _soundEffectList[sound];
        effect.Play();
        Destroy(effect, effect.clip.length + 0.5f); // 0.5f for good measure
    }

    public void PlayRandomize(float pitch, params int[] sound)
    {
        pitch = Random.Range(1 - pitch, 1 + pitch);
        int random = Random.Range(0, sound.Length);
        PlayAudio(sound[random]);
    }

    // Song intro
    public void PlayMusic(int number)
    {
        _songSource.loop = false;
        _songSource.clip = _songList[number][0];
        _songSource.Play();
        if (_songList[number][1] != null)
            StartCoroutine(PlayMusicLoop(number, _songSource.clip.length + 0.5f));
    }

    // Song loop
    private IEnumerator PlayMusicLoop(int number, float delay)
    {
        yield return new WaitForSeconds(delay);
        _songSource.loop = true;
        _songSource.clip = _songList[number][1];
        _songSource.Play();
    }
}