using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealtManager : MonoBehaviour
{

    public int vidaMaximaJugador;
    public int vidaActualJugador;
    // Start is called before the first frame update

    public int corazonesInicio = 3;
    public int maxNumCorazones = 10;
    public int vidaPorCorazon = 4;

    private bool flashActive;
    public float flashTime;
    private float flashCounter;
    private SpriteRenderer playerSprite;

    public Image[] HealthImage;
    public Sprite[] CorazonesSprite;

    public UIManager managerUi;

    void Start()
    {
        //vidaActualJugador = vidaMaximaJugador;
        vidaActualJugador = corazonesInicio * vidaPorCorazon;
        vidaMaximaJugador = maxNumCorazones * vidaPorCorazon;
        //checkVida();
        managerUi = FindObjectOfType<UIManager>();
        managerUi.checkVida();//maxNumCorazones, corazonesInicio);
        managerUi.UpdateVida();//vidaActualJugador, vidaPorCorazon);
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaActualJugador <= 0){
            gameObject.SetActive(false);
        }
        //Hace que el jugador se de unos flashasos
        if (flashActive) {
            if (flashCounter > flashTime * .66f){
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }else if (flashCounter > flashTime * .33f) {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            } else if( flashCounter > 0f) {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }else {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
    }

    //funcion que daña al jugador, llamada desde el script HurtPlayer
    public void HurtPlayer(int monto) {
        flashActive = true;
        flashCounter = flashTime;
        vidaActualJugador += monto;
        vidaActualJugador = Mathf.Clamp(vidaActualJugador, 0, corazonesInicio * vidaPorCorazon);
        managerUi.UpdateVida();//vidaActualJugador, vidaPorCorazon); 
    }
    //Función que cura al jugador, llamada desde el script HealPlayer
    public void HealPlayer(int monto) {
        vidaActualJugador += monto;
        vidaActualJugador = Mathf.Clamp(vidaActualJugador, 0, corazonesInicio * vidaPorCorazon);
        managerUi.UpdateVida();
    }
    
}
