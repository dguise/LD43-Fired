using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    [SerializeField] private Text _ScoreText;
    [SerializeField] private Text _DialogueQuestion;
    [SerializeField] private Text _DialogueAnswer;
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

    private string DialogueQuestion
    {
        get
        {
            return _DialogueQuestion.text;
        }
        set
        {
            if (DialogueQuestion != null)
                _DialogueQuestion.text = value;

        }
    }

    private string DialogueAnswer
    {
        get
        {
            return _DialogueAnswer.text;
        }
        set
        {
            if (DialogueAnswer != null)
                _DialogueAnswer.text = value;

        }
    }

    


    //private void Update()
    //{
    //    ScoreText = Time.timeSinceLevelLoad.ToString();
    //}

    Coroutine coroutine = null;
    public void ShowDialogue(string question, string answer)
    {
        // If a dialogue is showing, abort its coroutine and remove dialogue
        if (coroutine != null) {
            StopCoroutine(coroutine);
            DialogueQuestion = "";
            DialogueAnswer = "";
            _Dialogue.SetActive(false);
        }

        coroutine = StartCoroutine(ShowOrHideDialogue(question, answer));
    }

    IEnumerator ShowOrHideDialogue(string question, string answer, float secondsToWaitForHide = 5.0f)
    {
        DialogueQuestion = question;
        DialogueAnswer = answer;
        _Dialogue.SetActive(true);

        yield return new WaitForSeconds(secondsToWaitForHide);
        DialogueQuestion = "";
        DialogueAnswer = "";
        _Dialogue.SetActive(false);
    }
}
