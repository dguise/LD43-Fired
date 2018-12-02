using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnykeyToContinue : MonoBehaviour
{
    bool doOnce = true;
    void Update()
    {
        if (doOnce)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) > SceneManager.sceneCount ? 0 : SceneManager.GetActiveScene().buildIndex + 1);
                doOnce = false;
            }
        }
    }
}
