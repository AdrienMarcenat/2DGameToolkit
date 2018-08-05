﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour
{
    private Image m_FadeInOutImage;
    [SerializeField] float m_FadeSpeed;

    void Awake ()
    {
        m_FadeInOutImage = GetComponent<Image> ();
    }

    IEnumerator FadeIn ()
    {
        SetFadeInOutImageAlpha (1);
        yield return null;

        while (m_FadeInOutImage.color.a > 0)
        {
            AddToFadeInOutImageAlpha (-m_FadeSpeed);
            yield return null;
        }
    }

    IEnumerator FadeOut ()
    {
        SetFadeInOutImageAlpha (0);
        yield return null;

        while (m_FadeInOutImage.color.a < 1)
        {
            AddToFadeInOutImageAlpha (m_FadeSpeed);
            yield return null;
        }
    }

    private void AddToFadeInOutImageAlpha (float a)
    {
        Color c = m_FadeInOutImage.color;
        c.a += a;
        m_FadeInOutImage.color = c;
    }

    private void SetFadeInOutImageAlpha (float a)
    {
        Color c = m_FadeInOutImage.color;
        c.a = a;
        m_FadeInOutImage.color = c;
    }

    private void ChangeScene ()
    {
        StartCoroutine (FadeOut ());
    }

    private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        StartCoroutine (FadeIn ());
    }
}
