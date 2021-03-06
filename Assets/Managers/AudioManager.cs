﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Range(0, 1)]
    [Tooltip("The general volume all clips will be affected by")]
    public float volume = 1;
    [Tooltip("Leave as -1 to not play a song on start")]
    public int startingMusic = -1;

    private List<AudioClip> _songList = new List<AudioClip>();
    private List<AudioClip> _soundEffectList = new List<AudioClip>();

    private AudioSource _songSource;

    public enum enumSoundType
    {
        Chat,
        Glass,
        Kick,
        Call,
        Hire,
        Construction
    }


    void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(transform.parent.gameObject);
        }
    }

    void Start()
    {
        _soundEffectList = Resources.LoadAll<AudioClip>("chat").ToList();
        _songList = Resources.LoadAll<AudioClip>("Songs").ToList();
        _soundEffectList.AddRange(Resources.LoadAll<AudioClip>("glass"));
        _soundEffectList.AddRange(Resources.LoadAll<AudioClip>("kick"));
        _soundEffectList.AddRange(Resources.LoadAll<AudioClip>("call"));
        _soundEffectList.AddRange(Resources.LoadAll<AudioClip>("hire"));
        _soundEffectList.AddRange(Resources.LoadAll<AudioClip>("construction"));

        _songSource = gameObject.AddComponent<AudioSource>();
        _songSource.volume = volume;
        if (startingMusic >= 0)
            PlayMusic(startingMusic);
    }

    public void PlayAudio(int sound, float pitch = 1, float volume = 1)
    {
        AudioSource effect = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        //effect.pitch = pitch; FUCK ÄNDRINGAR AV PITCH, GÖR OM GÖR RÄTT
        // :help::argdrik:
        effect.clip = _soundEffectList[sound];
        effect.Play();
        effect.volume = volume;
        Destroy(effect, effect.clip.length + 0.5f); // 0.5f for good measure
    }

    public void PlayRandomize(float pitch, params int[] sound)
    {
        pitch = Random.Range(1 - pitch, 1 + pitch);
        int random = Random.Range(0, sound.Length);
        PlayAudio(sound[random], pitch);
    }

    public void PlayRandomize(enumSoundType aSoundType, float volume = 1)
    {
        switch (aSoundType)
        {
            case enumSoundType.Chat:
                PlayAudio(Random.Range(0, 4 + 1), 1, volume); //+1 för att få det inclusive
                break;
            case enumSoundType.Glass:
                PlayAudio(Random.Range(5, 8 + 1), 1, volume);
                break;
            case enumSoundType.Kick:
                PlayAudio(Random.Range(9, 10 + 1), 1, volume);
                break;
            case enumSoundType.Call:
                PlayAudio(11, 1, volume);
                break;
            case enumSoundType.Hire:
                PlayAudio(12, 1, volume);
                break;
            case enumSoundType.Construction:
                PlayAudio(13, 1, volume);
                break;
            default:
                break;
        }
    }

    // Song intro
    public void PlayMusic(int number)
    {
        _songSource.loop = false;
        _songSource.clip = _songList[number];
        _songSource.Play();
        StartCoroutine(LoopSong(number));
    }

    IEnumerator LoopSong(int number) {
        yield return new WaitForSeconds(_songSource.clip.length);
        _songSource.clip = _songList[number+1];
        _songSource.loop = true;
        _songSource.Play();
    } 
}