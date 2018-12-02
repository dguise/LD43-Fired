using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnykeyToContinue : MonoBehaviour
{
    bool doOnce = true;
    void Start() {
        FadeManager.Instance.FadeIn();
    }
    void Update()
    {
        if (doOnce)
        {
            if (Input.anyKeyDown)
            {
                FadeManager.Instance.FadeOut(LoadNextScene);
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(
            (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings - 1)
                ? 0 
                : SceneManager.GetActiveScene().buildIndex + 1
        );
        doOnce = false;
    }
}
