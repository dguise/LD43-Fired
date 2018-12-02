using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {
	void Update ()
    {
        this.transform.Rotate(
            Vector3.forward * (Settings.RoationSpeed * Time.deltaTime));
    }
}
