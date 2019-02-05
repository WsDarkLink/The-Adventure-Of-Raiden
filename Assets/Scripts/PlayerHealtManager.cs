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
    // variables utilizadas para daño progresivo
    private int damagePA, totalDamage, contadorHits;
    public bool progressiveActive;
    public float progressiveTime;
    private float progressiveCounter;
    //variables utilizadas para estado: Paralizado;
    public bool paralizadoActivo;
    private float contTiemPar;
    public bool invulnerable;

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
    
        //Utilizado para dañar al jugador de manera progresiva
        if (progressiveActive) {

            if (progressiveCounter <= 0f && contadorHits < totalDamage) {
                contadorHits++;
                HurtPlayer(damagePA);
                progressiveCounter = progressiveTime;
                if (contadorHits >= totalDamage) {
                    progressiveActive = false;
                }
            }

            progressiveCounter -= Time.deltaTime;
        }

        if (paralizadoActivo) {
            if (contTiemPar <= 0f) {
                GameManager.instance.fadingEntreAreas = false;
                paralizadoActivo = false;
            }else{
                contTiemPar -= Time.deltaTime;
            }
        }

    }

    //funcion que daña al jugador, llamada desde el script HurtPlayer
    public void HurtPlayer(int monto) {
        if (!invulnerable) {
            flashActive = true;
            flashCounter = flashTime;
            vidaActualJugador += monto;
            vidaActualJugador = Mathf.Clamp(vidaActualJugador, 0, corazonesInicio * vidaPorCorazon);
            managerUi.UpdateVida();//vidaActualJugador, vidaPorCorazon); 
        }
    }
    //Función que cura al jugador, llamada desde el script HealPlayer
    public void HealPlayer(int monto) {
        vidaActualJugador += monto;
        vidaActualJugador = Mathf.Clamp(vidaActualJugador, 0, corazonesInicio * vidaPorCorazon);
        managerUi.UpdateVida();
    }

    public void ProgressiveDammage(int monto, int montoMax) {
        progressiveActive = true;
        progressiveCounter = progressiveTime;
        damagePA = monto;
        totalDamage = montoMax;
        contadorHits = 0;
    }

    public void ParalizarJugador(float tiempoparalizado, int damage) {
        GameManager.instance.fadingEntreAreas = true;
        paralizadoActivo = true;
        contTiemPar = tiempoparalizado;
        HurtPlayer(damage);
    }

}
