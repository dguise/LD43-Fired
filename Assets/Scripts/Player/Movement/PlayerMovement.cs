using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[Range(0.01f, 0.1f)]
	public float speed = 0.05f;

	private float stairSpeedModifier = 0.5f;

	private bool OnStairs = false;
	private bool StairsActivated = false;
	private bool OnWalkDownArea = false;
	private bool OnWalkUpArea = false;
	private bool TouchingRightWall = false;
	private bool TouchingLeftWall = false;

	private SpriteRenderer sr;

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

	void Start() {
		sr = GetComponent<SpriteRenderer>();
	}

	void Update () {
		Vector2 velocity = new Vector2(0, 0);

		if (CanWalkHorizontal) {
			velocity.x = Input.GetAxisRaw("Horizontal") * speed;
			velocity = HandleLeftWall(velocity);
			velocity = HandleRightWall(velocity);
		}

		if (CanWalkVertical) {
			velocity.y = Input.GetAxisRaw("Vertical") * speed * stairSpeedModifier;
			velocity = HandleWalkDown(velocity);
			velocity = HandleWalkUp(velocity);
			velocity = HandleStairs(velocity);
		}

		if (velocity.x != 0)
			sr.flipX = velocity.x > 0;

		transform.Translate(velocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Arbetsplatser
            foreach (var apl in GameObject.FindGameObjectsWithTag(Assets.Scripts.Helper.Constants.Tags.Arbetsplats))
            {
                if (apl.GetComponentInChildren<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds))
                    apl.GetComponent<ArbetsplatsScript>().RemoveWorker();
            }
            //Box
            foreach (var shop in GameObject.FindGameObjectsWithTag(Assets.Scripts.Helper.Constants.Tags.PlaceToGetWorkers))
            {
                if (shop.GetComponentInChildren<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds))
                    Debug.Log("Du har köpt någon.");
            }
        }
            
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
			vel.x = speed * stairSpeedModifier * angle;
		}
		if (vel.y < 0) {
			vel.x = -speed * stairSpeedModifier * angle;
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
		if (col.tag == Assets.Scripts.Helper.Constants.Tags.Stairs) 				OnStairs = true;
		if (col.tag == Assets.Scripts.Helper.Constants.Tags.WalkDownArea)  	        OnWalkDownArea = true;
		if (col.tag == Assets.Scripts.Helper.Constants.Tags.WalkUpArea) 		    OnWalkUpArea = true;
		if (col.tag == "LeftWall")			                                        TouchingLeftWall = true;
		if (col.tag == "RightWall")			                                        TouchingRightWall = true;
	}

	void OnTriggerExit2D(Collider2D col) {
		Debug.Log(col.tag);
		if (col.tag == Assets.Scripts.Helper.Constants.Tags.Stairs) 				OnStairs = false;
		if (col.tag == Assets.Scripts.Helper.Constants.Tags.Stairs) 				StairsActivated = false;
		if (col.tag == Assets.Scripts.Helper.Constants.Tags.WalkDownArea) 	        OnWalkDownArea = false;
		if (col.tag == Assets.Scripts.Helper.Constants.Tags.WalkUpArea) 	    	OnWalkUpArea = false;
		if (col.tag == "LeftWall")		                                            TouchingLeftWall = false;
		if (col.tag == "RightWall")		                                            TouchingRightWall = false;
	}
}
