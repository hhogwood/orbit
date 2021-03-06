﻿using UnityEngine;
using System.Collections;

public class OrbitScript : MonoBehaviour
{
    private int colorInt = 0;
    private float fadeFloat = 1f;
    private bool fade = false;

    void Start()
    {
        StartCoroutine(StartFade());
    }

    void FixedUpdate()
    {
        colorInt++;

        if (colorInt > 359)
        {
            colorInt = 0;
        }

        GetComponent<Renderer>().material.color = HSVColor.HSVToRGB(colorInt, .4f, 1f);

        

        if (fade)
        {
            fadeFloat -= .005f;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeFloat);

            if (fadeFloat < 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(2f);
        fade = true;
    }
}

