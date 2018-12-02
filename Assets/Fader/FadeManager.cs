using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    Image fadeImage;

    private Color invisible = new Color(0, 0, 0, 0);
    private Color visible = new Color(0, 0, 0, 1);

    private float speed = 1;

    private Coroutine fadeRoutine = null;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // We know that the GameManagers object is being saved already
            // DontDestroyOnLoad(transform.parent.gameObject);
        }

        fadeImage = GetComponent<Image>();
    }

    public void FadeOut(Action doneCallback = null)
    {
        if (fadeRoutine == null) {
            fadeRoutine = StartCoroutine(_FadeOut(doneCallback));
        }
    }
    private IEnumerator _FadeOut(Action doneCallback)
    {
        float tick = 0f;
        while (fadeImage.color != visible)
        {
            tick += Time.deltaTime * speed;
            fadeImage.color = Color.Lerp(invisible, visible, tick);
            yield return new WaitForEndOfFrame();
        }

        if (doneCallback != null)
            doneCallback();
        fadeRoutine = null;
    }

    public void FadeIn(Action doneCallback = null)
    {
        if (fadeRoutine == null)
            fadeRoutine = StartCoroutine(_FadeIn(doneCallback));
    }
    private IEnumerator _FadeIn(Action doneCallback)
    {
        float tick = 0f;
        while (fadeImage.color != invisible)
        {
            tick += Time.deltaTime * speed;
            fadeImage.color = Color.Lerp(visible, invisible, tick);
            yield return new WaitForEndOfFrame();
        }

        if (doneCallback != null)
            doneCallback();
        fadeRoutine = null;
    }
}
