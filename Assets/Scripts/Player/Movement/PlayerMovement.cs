using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[Range(0.01f, 0.1f)]
	public float speed = 0.05f;

	private bool OnStairs = false;
	private bool StairsActivated = false;
	private bool OnWalkDownArea = false;
	private bool OnWalkUpArea = false;

	void Start () {
		
	}
	
	void Update () {
		float y = 0;
		if (OnStairs || OnWalkDownArea || OnWalkUpArea) {
			y = Input.GetAxisRaw("Vertical") * speed;
			
			if (OnWalkDownArea && !OnStairs) {
				if (y > 0) {
					y = 0;
				}
			}

			if (OnWalkUpArea && !OnStairs) {
				if (y < 0) {
					y = 0;
				}
			}

			if (y != 0) {
				StairsActivated = true;
			}
		}

		float x = 0;
		if (!StairsActivated) {
			 x = Input.GetAxisRaw("Horizontal") * speed;
		}

		transform.Translate(x, y, 0);
	}


	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Stairs") 				OnStairs = true;
		if (col.tag == "WalkDownArea") 	OnWalkDownArea = true;
		if (col.tag == "WalkUpArea") 		OnWalkUpArea = true;
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "Stairs") 				OnStairs = false;
		if (col.tag == "Stairs") 				StairsActivated = false;
		if (col.tag == "WalkDownArea") 	OnWalkDownArea = false;
		if (col.tag == "WalkUpArea") 		OnWalkUpArea = false;
	}
}
