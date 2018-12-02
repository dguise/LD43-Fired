using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool OnStairs = false;
    private bool StairsActivated = false;
    private bool OnWalkDownArea = false;
    private bool OnWalkUpArea = false;
    private bool TouchingRightWall = false;
    private bool TouchingLeftWall = false;
    private bool frozen = false;

    private SpriteRenderer sr;
    [SerializeField] private SpriteRenderer srChild;
    [SerializeField] private GameObject babyPrefab;
    private float kickingPowah = 7.0f * 2f;
    private Animator anim;

    private bool CanWalkVertical
    {
        get
        {
            return OnStairs || OnWalkDownArea || OnWalkUpArea;
        }
    }

    private bool CanWalkHorizontal
    {
        get
        {
            return !StairsActivated;
        }
    }

    public bool CarryingWorker
    {
        get { return srChild.gameObject.activeSelf; }
    }


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
		if (FadeManager.Instance != null)
			FadeManager.Instance.FadeIn();
    }


    void Update()
    {
        Vector2 velocity = new Vector2(0, 0);

        if (CanWalkHorizontal)
        {
            velocity.x = Input.GetAxisRaw("Horizontal") * Settings.Speed;
            velocity = HandleLeftWall(velocity);
            velocity = HandleRightWall(velocity);
        }

        if (CanWalkVertical)
        {
            velocity.y = Input.GetAxisRaw("Vertical") * Settings.Speed * Settings.StairSpeedModifier;
            velocity = HandleWalkDown(velocity);
            velocity = HandleWalkUp(velocity);
            velocity = HandleStairs(velocity);
        }

        var isRunning = velocity.x != 0;
        anim.SetBool("Running", isRunning);
        if (isRunning)
        {
            sr.flipX = velocity.x > 0;
            srChild.flipX = !sr.flipX;
            srChild.transform.localPosition = (srChild.flipX)
                ? new Vector3(0.44f, srChild.transform.localPosition.y, srChild.transform.localPosition.z)
                : new Vector3(-0.44f, srChild.transform.localPosition.y, srChild.transform.localPosition.z);
        }

        transform.Translate(velocity);

        if (Input.GetKeyDown(KeyCode.Space) && !OnStairs && !CarryingWorker)
        {
            var gotWorker = TryGetWorkerFromBox();
            if (!gotWorker)
                TryKickWorker();
        }
    }

    bool TryKickWorker() {
        foreach (var apl in GameObject.FindGameObjectsWithTag(Tags.Arbetsplats))
        {
            if (apl.GetComponentInChildren<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds))
            {
                if (apl.GetComponent<ArbetsplatsScript>().Aktiv)
                {
                    apl.GetComponent<ArbetsplatsScript>().RemoveWorker();
                    KickTheBaby();
                    return true;
                }
            }
        }
        return false;
    }

    bool TryGetWorkerFromBox() {
        foreach (var shop in GameObject.FindGameObjectsWithTag(Tags.PlaceToGetWorkers))
        {
            if (shop.GetComponentInChildren<Renderer>().bounds.Intersects(this.GetComponent<Renderer>().bounds)) {
                var baby = (GameObject)GameObject.Instantiate(babyPrefab, transform.position + Vector3.up * 8f, transform.rotation);
                StartCoroutine(MoveTowardsPlayer(baby));
                anim.SetBool("Running", false);
                AudioManager.Instance.PlayRandomize(AudioManager.enumSoundType.Call);
                return true;
            }
        }
        return false;
    }

    private IEnumerator MoveTowardsPlayer(GameObject baby) {
        float step = 0;
        float speed = 6;
        while (Vector2.Distance(baby.transform.position, transform.position) > 0.2f) {
            step += Time.deltaTime * speed;
            Debug.Log(step);
            baby.transform.position = Vector2.MoveTowards(transform.position + Vector3.up * 8f, transform.position, step);

            yield return new WaitForEndOfFrame();
        }
        GameObject.Destroy(baby);
        srChild.gameObject.SetActive(true);
    }

    private void KickTheBaby()
    {
        var baby = (GameObject)GameObject.Instantiate(babyPrefab, this.transform.position, transform.rotation);
        baby.GetComponent<Rigidbody2D>().simulated = true;
        baby.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.transform.position.x < 0 ? -kickingPowah : kickingPowah, 3), ForceMode2D.Impulse);
        GameObject.Destroy(baby, 3.0f);
        AudioManager.Instance.PlayRandomize(AudioManager.enumSoundType.Kick);
    }

    public void DropOffWorker()
    {
        srChild.gameObject.SetActive(false);
        AudioManager.Instance.PlayRandomize(AudioManager.enumSoundType.Hire);
    }

    #region collision movement restrictions
    Vector2 HandleWalkDown(Vector2 vel)
    {
        if (OnWalkDownArea && !OnStairs)
            if (vel.y >= 0)
            {
                vel.y = 0;
            }

        return vel;
    }

    Vector2 HandleWalkUp(Vector2 vel)
    {
        if (OnWalkUpArea && !OnStairs)
            if (vel.y <= 0)
            {
                vel.y = 0;
            }

        return vel;
    }

    Vector2 HandleStairs(Vector2 vel)
    {
        float angle = 0.74f;
        if (vel.y != 0)
        {
            StairsActivated = true;
        }
        if (vel.y > 0)
        {
            vel.x = Settings.Speed * Settings.StairSpeedModifier * angle;
        }
        if (vel.y < 0)
        {
            vel.x = -Settings.Speed * Settings.StairSpeedModifier * angle;
        }
        return vel;
    }

    Vector2 HandleRightWall(Vector2 vel)
    {
        if (TouchingRightWall)
            if (vel.x > 0)
            {
                vel.x = 0;
            }

        return vel;
    }

    Vector2 HandleLeftWall(Vector2 vel)
    {
        if (TouchingLeftWall)
            if (vel.x < 0)
                vel.x = 0;

        return vel;
    }
    #endregion


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.Stairs) OnStairs = true;
        if (col.tag == Tags.WalkDownArea) OnWalkDownArea = true;
        if (col.tag == Tags.WalkUpArea) OnWalkUpArea = true;
        if (col.tag == Tags.LeftWall) TouchingLeftWall = true;
        if (col.tag == Tags.RightWall) TouchingRightWall = true;

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == Tags.Stairs) OnStairs = false;
        if (col.tag == Tags.Stairs) StairsActivated = false;
        if (col.tag == Tags.WalkDownArea) OnWalkDownArea = false;
        if (col.tag == Tags.WalkUpArea) OnWalkUpArea = false;
        if (col.tag == Tags.LeftWall) TouchingLeftWall = false;
        if (col.tag == Tags.RightWall) TouchingRightWall = false;
    }
}
