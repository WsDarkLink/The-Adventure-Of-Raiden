using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public static UIFade instance;

    public Image fadeScreen;
    public float fadeSpeed;
    public bool debePonerseNegro;
    public bool debeSerTransparente;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debePonerseNegro)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f) {
                debePonerseNegro = false;
            }
        }
        if (debeSerTransparente)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f) {
                debeSerTransparente = false;
            } 
        }
    }

    public void PonerEnNegro() {
        debePonerseNegro = true;
        debeSerTransparente = false;
    }
    public void PonerTransparente() {
        debePonerseNegro = false;
        debeSerTransparente = true;
    }

}
