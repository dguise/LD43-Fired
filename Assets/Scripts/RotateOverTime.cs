using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {

    public float roationSpeed = 3.0f;
	void Update ()
    {
        this.transform.Rotate(Vector3.forward * (roationSpeed * Time.deltaTime));
    }
}
