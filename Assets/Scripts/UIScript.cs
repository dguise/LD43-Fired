using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    [SerializeField] private Text scoreText;
    public string ScoreText
    {
        get
        {
            return scoreText.text;
        }
        set
        {
            if (scoreText != null)
            scoreText.text = value;
        }
    }

    private void Update()
    {
        ScoreText = Time.timeSinceLevelLoad.ToString();
    }
}
