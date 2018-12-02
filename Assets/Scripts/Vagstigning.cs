using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vagstigning : MonoBehaviour {
	public float riseSpeed = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(0f, riseSpeed * Time.deltaTime, 0f);
	}

	void OnTriggerEnter2D(Collider2D col) {
		SceneManager.LoadScene("GameOverScene");
	}
}
