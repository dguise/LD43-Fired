using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbetare : MonoBehaviour {

    [SerializeField] private TextMesh rpText;
    public bool Awesome { get; set; }
    public void DoBadStuff()
    {
        StartCoroutine(ShowrpText());
    }

    IEnumerator ShowrpText()
    {
        this.rpText.gameObject.SetActive(true);
        this.rpText.text = ListOfBadRPTexts[Random.Range(0, ListOfBadRPTexts.Count - 1)];
        yield return new WaitForSeconds(2.0f);
        this.rpText.text = "";
        this.rpText.gameObject.SetActive(false);
    }

    private List<string> ListOfBadRPTexts = new List<string>
    {
        "Hahaha!",
        "La la la",
        "♪-♫-♪",
        "Zzzzz",
        "Check this gif!",
        ":joy: :ok_hand:",
        "reddit.com",
        "OMG!",
        "Here's a pic of my kid",
        ":(",
    };
}
