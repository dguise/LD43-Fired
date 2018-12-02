using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    #region Utility
    public delegate void Progress(float progress);
    public event Progress OnProgress;

    private int _internalProgress = 0;
    private int _loadingCategories = 3; // How many times IncrementProgress() will be called while preloading.
    #endregion


    public GameObject Player { get; private set; }

    public List<AudioClip[]> Songs { get; private set; }
    public List<AudioClip> Sounds { get; private set; }

    public List<GameObject> ParticleEffects { get; private set; }

    private void Awake()
    {
        // // Player
        // Player = Resources.Load<GameObject>("Player");
        // IncrementProgress();

        // // Audio - Music assumes files are named "ThemeSong-intro" & "ThemeSong-loop".
        // If no -intro is found, it searches without the appended string.
        // string[] songsToLoad = { "ThemeSong", "Battle", "Night" };
        // foreach (string song in songsToLoad)
        // {
        //     Songs.Add(
        //         new AudioClip[] {
        //             (Resources.Load<AudioClip>(song + "-intro")) ?? Resources.Load<AudioClip>(song),
        //             (Resources.Load<AudioClip>(song + "-loop"))
        //             });
        // }

        Sounds = new List<AudioClip>();
        string[] soundsToLoad = { "chat-1", "chat-2", "chat-3", "chat-4", "chat-5" };
        foreach (string sound in soundsToLoad)
        {
            Sounds.Add(Resources.Load(sound) as AudioClip);   
        }
        Debug.Log(Sounds.Count);
        // IncrementProgress();

        // foreach (var particleName in Enum.GetNames(typeof(ParticleTypes)))
        // {
        //     ParticleEffects.Add(Resources.Load<GameObject>(particleName));
        // }
    }

    private void IncrementProgress()
    {
        if (OnProgress != null)
            OnProgress(++_internalProgress / _loadingCategories);
    }
}
