using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetsplatsScript : MonoBehaviour {

    [SerializeField] private bool hasWorker = false;
    [SerializeField] private bool hasAwesomeWorker = false;

    public bool Aktiv
    {
        get { return hasWorker; } //Kanske lägga på mer logik framöver
    }
    public bool HasAwesomeWorker
    {
        get { return hasAwesomeWorker; }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (IsPlayer(col))
            AddWorker();
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (IsPlayer(col))
            RemoveWorker();
    }

    private bool IsPlayer(Collider2D col)
    {
        return (col.tag == "Player");
    }

    public void AddWorker()
    {
        hasWorker = true;
        hasAwesomeWorker = (Random.value > 0.5f); //Hälften av gångerna är det en awesome person
    }

    public void RemoveWorker()
    {
        hasWorker = false;
    }
}
