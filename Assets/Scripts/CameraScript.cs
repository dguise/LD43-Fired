using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    float startingY = 0;

    private void Start()
    {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag(Tags.Player);
        startingY = this.transform.position.y - Player.transform.position.y;
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, startingY + Player.transform.position.y, this.transform.position.z);
    }

}
