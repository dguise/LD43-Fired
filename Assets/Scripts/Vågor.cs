﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vågor : MonoBehaviour
{
    public float radius = 0.1f;
    public float speed = 5.0f;
    private Vector2 mittpunkt;
    private float angle;


    void Start()
    {
        this.mittpunkt = transform.position;
    }

    void Update()
    {
        angle += speed * Time.deltaTime;
        transform.position = mittpunkt + (new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius);
    }
}
