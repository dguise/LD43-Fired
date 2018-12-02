﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform vaning;
    public float vaningsHojd;

    [SerializeField] private List<Transform> vaningar;

    private UIScript GUIManager;

    //1 dollar per bra worker
    const int inkomstPerAwesomeArbetsplats = 1;
    //0 dollar per dålig
    const int inkomstPerArbetsplats = 0;

    private int _antalGuldmyntIPengabingen = 0;
    int antalGuldmyntIPengabingen
    {
        get { return _antalGuldmyntIPengabingen; }
        set
        {
            _antalGuldmyntIPengabingen = value;
            GUIManager.ScoreText = "$" + value.ToString() + " / " + RikedomsMal;
        }
    }

    private int RikedomsMal = 100;

    private void Start()
    {
        GUIManager = GameObject.FindObjectOfType<UIScript>();
        InvokeRepeating("BeraknaInkomst", 0, 1.0f);
    }

    private void Update()
    {
        
    }

    void BeraknaInkomst()
    {
        var arbetsplatser = GameObject.FindObjectsOfType<ArbetsplatsScript>();

        int inkomst = 0;
        foreach (var apl in arbetsplatser)
        {
            inkomst = 10;
            if (apl.Aktiv)
                inkomst += apl.HasAwesomeWorker ? inkomstPerAwesomeArbetsplats : inkomstPerArbetsplats;
        }

        antalGuldmyntIPengabingen += inkomst;

        if (antalGuldmyntIPengabingen >= RikedomsMal) {
            RikedomsMal += 100;
            AddVaning();
        }
    }

    void AddVaning()
    {
        Transform v = Instantiate(vaning, transform.position + new Vector3(0f, vaningar.Count * vaningsHojd, 0f), Quaternion.identity);
        vaningar.Add(v);
    }
}
