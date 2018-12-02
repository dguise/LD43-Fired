using System.Collections;
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
        PlayAudio(sound[random], pitch);
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