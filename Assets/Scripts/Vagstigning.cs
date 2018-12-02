using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vagstigning : MonoBehaviour {
	public float riseSpeed = 0.2f;

	void Update () {
        transform.position = transform.position + new Vector3(0f, riseSpeed * Time.deltaTime, 0f);
	}

	void OnTriggerEnter2D(Collider2D col) {
		FadeManager.Instance.FadeOut(() => {
			SceneManager.LoadScene("GameOverScene");
		});
		
	}
}
