using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static bool UIExist;
    // Start is called before the first frame update

    public Image[] HealthImage;
    public Sprite[] CorazonesSprite;
    public PlayerHealtManager healthManager;
    public static UIManager instance;

    void Start()
    {
        //checkVida();
        //UpdateVida();
         if (!UIExist)
         {
             UIExist = true;
            instance = this;
             DontDestroyOnLoad(transform.gameObject);
             UpdateVida();
         }
         else
         {
             Destroy(gameObject);
         }
        healthManager = FindObjectOfType<PlayerHealtManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkVida()//int maxNumCorazones, int corazonesInicio)
    {
        int maxNumCorazones = healthManager.maxNumCorazones;
        int corazonesInicio = healthManager.corazonesInicio;
        for (int i = 0; i < maxNumCorazones; i++)
        {
            if (corazonesInicio <= i)
            {
                HealthImage[i].enabled = false;
            }
            else
            {
                HealthImage[i].enabled = true;
            }
        }
    }
    
    public void UpdateVida()//int vidaActualJugador, int vidaPorCorazon)
    {
        int vidaActualJugador = healthManager.vidaActualJugador;
        int vidaPorCorazon = healthManager.vidaPorCorazon;
        bool vacio = false;
        int i = 0;
        foreach (Image imagen in HealthImage)
        {
            if (vacio)
            {
                imagen.sprite = CorazonesSprite[0];
            }
            else
            {
                i++;
                if (vidaActualJugador >= i * vidaPorCorazon)
                {
                    imagen.sprite = CorazonesSprite[CorazonesSprite.Length - 1];
                }
                else
                {
                    int vidaActualCorazon = (int)(vidaPorCorazon - (vidaPorCorazon * i - vidaActualJugador)); // Ejemplo 4 - (4 * 4 - 12) = 4     4 - (4 * 3 - 11) = 3
                    int vidaPorImagen = vidaPorCorazon / (CorazonesSprite.Length - 1); //tamaño del arreglo de sprites 4 /4 = 1                   4/4
                    int imagenIndex = vidaActualCorazon / vidaPorImagen; //4/1 = 4                                                                 
                    imagen.sprite = CorazonesSprite[imagenIndex];
                    vacio = true;
                }
            }
        }
    }


}
