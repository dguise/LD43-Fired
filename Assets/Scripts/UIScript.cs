using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    [SerializeField] private Text _ScoreText;
    [SerializeField] private Text _DialogueText;
    [SerializeField] private GameObject _Dialogue;
    public string ScoreText
    {
        get
        {
            return _ScoreText.text;
        }
        set
        {
            if (_ScoreText != null)
            _ScoreText.text = value;
        }
    }

    private string DialogueText
    {
        get
        {
            return _DialogueText.text;
        }
        set
        {
            if (DialogueText != null)
                _DialogueText.text = value;

        }
    }


    private void Update()
    {
        ScoreText = Time.timeSinceLevelLoad.ToString();

        if (Input.anyKeyDown)
        {
            ShowDialogue("- Hej\n\n                        - Jag är en katt. Meow.");
        }
    }


    public void ShowDialogue(string textToShow)
    {
        StartCoroutine(ShowOrHideDialogue(true, 0, 2, textToShow));
    }

    IEnumerator ShowOrHideDialogue(bool show, float secondsToWaitForShow, float secondsToWaitForHide = 2.0f, string textToShow = "...")
    {
        yield return new WaitForSeconds(secondsToWaitForShow);
        DialogueText = textToShow;
        _Dialogue.SetActive(show);

        yield return new WaitForSeconds(secondsToWaitForHide);
        DialogueText = "";
        _Dialogue.SetActive(false);
    }
}
