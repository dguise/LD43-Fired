using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnykeyNextScene : MonoBehaviour {

	void Start() {
	}
	private bool _notRunOnce = true;
	void Update () {
		if (Input.anyKeyDown && _notRunOnce) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
