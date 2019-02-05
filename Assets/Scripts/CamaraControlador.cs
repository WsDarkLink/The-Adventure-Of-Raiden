using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CamaraControlador : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform objetivo;

    public Tilemap mapa;
    private Vector3 abajoIzquierdalimite;
    private Vector3 arribaDerechalimite;
    //limites de la camara
    private float mitadAltura;
    private float mitadAnchura;
    //variables utilizadas por el AudioManager
    public int musicToPlay;
    private bool musicStarted;

    public 
    void Start()
    {
        //objetivo = PlayerControlador.instance.transform;
        objetivo = FindObjectOfType<PlayerControlador>().transform;

        mitadAltura = Camera.main.orthographicSize;
        mitadAnchura = mitadAltura * Camera.main.aspect;

        abajoIzquierdalimite = mapa.localBounds.min + new Vector3(mitadAnchura, mitadAltura, 0f);
        arribaDerechalimite = mapa.localBounds.max + new Vector3(-mitadAnchura, -mitadAltura, 0f);

        PlayerControlador.instance.SetBounds(mapa.localBounds.min, mapa.localBounds.max);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);
        //mantener la camara adentro del mapa
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, abajoIzquierdalimite.x, arribaDerechalimite.x), Mathf.Clamp(transform.position.y, abajoIzquierdalimite.y, arribaDerechalimite.y), transform.position.z);
        if (!musicStarted) {
            musicStarted = true;
            AudioManager.instance.PlayBGM(musicToPlay);
        }
    }
}
