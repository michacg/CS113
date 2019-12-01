using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage.canvasRenderer.SetAlpha(0.0f);
       
        fadeIn();
    }

    // Update is called once per frame
    void fadeIn()
    {
        fadeImage.CrossFadeAlpha(1, 2, false);
    }
}
