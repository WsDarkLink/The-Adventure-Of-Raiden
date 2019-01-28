using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalidaArea : MonoBehaviour
{
    // Start is called before the first frame update
    public string areaToLoad;
    public string areaTransicion;

    public EntradaArea laEntrada;

    public float esperaParaCargar = 1f;
    private bool shouldLoadAfterFade;

    void Start()
    {
        laEntrada.transicionName = areaTransicion; 
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade == true) {
            esperaParaCargar -= Time.deltaTime;
            if (esperaParaCargar <= 0) {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            //SceneManager.LoadScene(areaToLoad);
            shouldLoadAfterFade = true;
            GameManager.instance.fadingEntreAreas = true;
            UIFade.instance.PonerEnNegro();

            PlayerControlador.instance.areaTransicion = areaTransicion;
        }
    }
}
