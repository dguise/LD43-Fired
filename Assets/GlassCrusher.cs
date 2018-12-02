using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCrusher : MonoBehaviour {
	private ParticleSystem ps;
	private bool triggered = false;

	void Start() {
		ps = GetComponent<ParticleSystem>();
		ps.Stop();
	} 

	void OnTriggerEnter2D(Collider2D col) {
		if (!triggered) {
			ps.Play();
			triggered = true;
			AudioManager.Instance.PlayRandomize(0.5f, 5, 6, 7, 8);
		}
	}
}
