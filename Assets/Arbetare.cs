﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbetare : MonoBehaviour {

    [SerializeField] private TextMesh rpText;
    private bool awesome = false;
    public bool seeminglyAwesome
    {
        get { return awesome; }
    }
    private float awesomeFactor = 1.0f;

    public void SetAwesome(bool value)
    {
        awesome = value;
    }

    public void DoBadStuff()
    {
        StartCoroutine("ShowrpText");
    }

    IEnumerator ShowrpText()
    {
        this.rpText.gameObject.SetActive(true);
        this.rpText.text = "ZzzZz...";
        yield return new WaitForSeconds(2.0f);
        this.rpText.text = "";
        this.rpText.gameObject.SetActive(false);
    }
}
