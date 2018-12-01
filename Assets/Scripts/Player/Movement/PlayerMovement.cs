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
	private bool TouchingRightWall = false;
	private bool TouchingLeftWall = false;

	private bool CanWalkVertical { 
		get { 
			return OnStairs || OnWalkDownArea || OnWalkUpArea;
	 	} 
	}
	
	private bool CanWalkHorizontal {
		get {
			return !StairsActivated;
		}
	}

	void Update () {
		Vector2 velocity = new Vector2(0, 0);

		if (CanWalkHorizontal) {
			velocity.x = Input.GetAxisRaw("Horizontal") * speed;
			velocity = HandleLeftWall(velocity);
			velocity = HandleRightWall(velocity);
		}

		if (CanWalkVertical) {
			velocity.y = Input.GetAxisRaw("Vertical") * speed;
			velocity = HandleWalkDown(velocity);
			velocity = HandleWalkUp(velocity);
			velocity = HandleStairs(velocity);
		}

		transform.Translate(velocity);
	}

#region collision movement restrictions
	Vector2 HandleWalkDown(Vector2 vel) {
		if (OnWalkDownArea && !OnStairs)
			if (vel.y >= 0) {
				vel.y = 0;
			}

		return vel;
	}

	Vector2 HandleWalkUp(Vector2 vel) {
		if (OnWalkUpArea && !OnStairs)
			if (vel.y <= 0) {
				vel.y = 0;
			}

		return vel;
	}

	Vector2 HandleStairs(Vector2 vel) {
		float angle = 0.74f;
		if (vel.y != 0) {
			StairsActivated = true;
		}
		if (vel.y > 0) {
			vel.x = speed * angle;
		}
		if (vel.y < 0) {
			vel.x = -speed * angle;
		}
		return vel;
	}

	Vector2 HandleRightWall(Vector2 vel) {
		if (TouchingRightWall)
			if (vel.x > 0) {
				vel.x = 0;
			}

		return vel;
	}

	Vector2 HandleLeftWall(Vector2 vel) {
		if (TouchingLeftWall)
			if (vel.x < 0)
				vel.x = 0;

		return vel;
	}
#endregion


	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Stairs") 				OnStairs = true;
		if (col.tag == "WalkDownArea") 	OnWalkDownArea = true;
		if (col.tag == "WalkUpArea") 		OnWalkUpArea = true;
		if (col.tag == "LeftWall")			TouchingLeftWall = true;
		if (col.tag == "RightWall")			TouchingRightWall = true;
	}

	void OnTriggerExit2D(Collider2D col) {
		Debug.Log(col.tag);
		if (col.tag == "Stairs") 				OnStairs = false;
		if (col.tag == "Stairs") 				StairsActivated = false;
		if (col.tag == "WalkDownArea") 	OnWalkDownArea = false;
		if (col.tag == "WalkUpArea") 		OnWalkUpArea = false;
		if (col.tag == "LeftWall")			TouchingLeftWall = false;
		if (col.tag == "RightWall")			TouchingRightWall = false;
	}
}
