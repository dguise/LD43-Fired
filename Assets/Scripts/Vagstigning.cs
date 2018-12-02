using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vagstigning : MonoBehaviour {
	
	void Update () {
        transform.position = transform.position + new Vector3(0f, Settings.RiseSpeed * Time.deltaTime, 0f);
	}

	void OnTriggerEnter2D(Collider2D col) {
		
		if (col.tag != Tags.Player) {
			if (FadeManager.Instance != null) {
				FadeManager.Instance.FadeOut(() => {
					LoadGameOverScene();
				});
			} else {
				LoadGameOverScene();
			}
		} else {
			var ap = col.GetComponent<ArbetsplatsScript>();
			if (ap != null)
				ap.RemoveWorker();
		}
	}

	void LoadGameOverScene() {
		SceneManager.LoadScene("GameOverScene");
	}
}
