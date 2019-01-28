using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaArea : MonoBehaviour
{
    // Start is called before the first frame update
    public string transicionName;

    void Start()
    {
        if (transicionName == PlayerControlador.instance.areaTransicion) {
            PlayerControlador.instance.transform.position = transform.position;
        }

        UIFade.instance.PonerTransparente();
        GameManager.instance.fadingEntreAreas = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
