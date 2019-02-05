using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlador : MonoBehaviour
{
    //variables para movimiento
    public float velocidadMovimiento;
    private float velocidadMovActual;
    public Vector2 lastMove;
    private Vector2 moveInput;
    private Animator animator;
    private bool playermoving;
    private Rigidbody2D playerRigidBody; 
    private PlayerHealtManager PHManager;
    public bool puedoMoverme;
    private static bool playerExist;

    //variables para atacar
    public float atackTime, tAnimShu;
    private float atackTimeCounter;
    private bool atacando;

    //instancia del mismo GO
    public static PlayerControlador instance;
    //variable utilizada para cambiar de zonas
    public string areaTransicion;

    //variables para no salir de mapa
    private Vector3 abajoIzquierdalimite;
    private Vector3 arribaDerechalimite;

    //variables de ataques
    private bool btnKPress, btnLPress;
    private EnfriamientoObjetos enfriamientoObjetos;
    //variables para disparar Arco:  
    public int damageArco;
    private bool arcoActivo, arcoCargado, cdArcoCom, arcoInstanciado, dañoArcoX2, arcoInstancia;
    private float cdArco = 3f, cargaArco = 2f, contadorArco, contadorCargaArco, contadorAnimArco;
    private string direccionFlecha;
    private Vector3 dirmovFlecha;
    public float tAnimFlecha = .35f;
    //variables shuriken shurikenActivo, cdShuCom, contadorShuriken, contadorTAnimShu,  
    private bool shurikenActivo, cdShuCom;
    private float cdShuriken = 1f, contadorShuriken, contadorTAnimShu;
    //Variables Wakizashi wakizashiActivo, cdWakiCom, cdWaki, tiempoWaki, contadorCdWaki, contadorTiemWaki,
    private bool wakizashiActivo;
    private bool cdWakiCom;
    private float cdWaki = 2f, tiempoWaki = 3f;
    private float contadorCdWaki = 0, contadorTiemWaki;
    //variables Bomba  bombaActivo 
    private bool bombaActivo;

    //prefabs de items para arrojar
    public GameObject shuriken, arco, bomba;
    public GameObject instanciaShu, instanciaArco, instanciaBomba;   

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        PHManager = GetComponent<PlayerHealtManager>();
        enfriamientoObjetos = GetComponent<EnfriamientoObjetos>();
        puedoMoverme = true;
        //evitar que el jugador se duplique y/o desaparezca
         if (!playerExist)
         {
             playerExist = true;
             DontDestroyOnLoad(transform.gameObject);
             instance = this;
            // UpdateVida();
         }
         else
         {
             Destroy(gameObject);            
         }
    }

    // Update is called once per frame
    void Update()
    {
        playermoving = false;

        if (!puedoMoverme)
        {
            playerRigidBody.velocity = Vector2.zero;
            return;
        }
        //movimiento del personaje
        if (!atacando) {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (moveInput != Vector2.zero)
            {
                playerRigidBody.velocity = new Vector2(moveInput.x * velocidadMovimiento, moveInput.y * velocidadMovimiento);
                playermoving = true;
                lastMove = moveInput;
            } else {
                playerRigidBody.velocity = Vector2.zero;
            }
            //verificacion de enfriamiento de items
            cdArcoCom = enfriamientoObjetos.EstadoCdrArco();
            cdShuCom = enfriamientoObjetos.EstadoCdrShuriken();
            cdWakiCom = enfriamientoObjetos.EstadoCdrWaki();
            //verificacion de tecla presionada K
            if (Input.GetKeyDown(KeyCode.K)) {
                btnKPress = true;
                if (GameManager.instance.playerStats.itemK == "Espada Dragon")
                {
                    AtaqueEspada();
                }

                else if (GameManager.instance.playerStats.itemK == "Arco" && cdArcoCom && GameManager.instance.numDeItems[4] > 0)
                {
                    GameManager.instance.RemoveItem("Arco");
                    DireccionDañoAtaqueArco();
                    arcoInstancia = false;
                }
                else if (GameManager.instance.playerStats.itemK == "Bomba" && GameManager.instance.numDeItems[3] > 0)
                {
                    GameManager.instance.RemoveItem("Bomba");
                    if (lastMove.x == 1 && lastMove.y == 0)
                    {
                        AtaqueBomba("derecha");
                    }
                    else if (lastMove.x == -1 && lastMove.y == 0)
                    {
                        AtaqueBomba("izquierda");
                    }
                    else if (lastMove.x == 0 && lastMove.y == 1)
                    {
                        AtaqueBomba("arriba");
                    }
                    else if (lastMove.x == 0 && lastMove.y == -1)
                    {
                        AtaqueBomba("abajo");
                    }
                }
                else if (GameManager.instance.playerStats.itemK == "Shuriken" && cdShuCom && GameManager.instance.numDeItems[5] > 0)
                {
                    GameManager.instance.RemoveItem("Shuriken");
                    if (lastMove.x == 1 && lastMove.y == 0)
                    {
                        LanzarShuriken("derecha", arribaDerechalimite);
                    }
                    else if (lastMove.x == -1 && lastMove.y == 0)
                    {
                        LanzarShuriken("izquierda", abajoIzquierdalimite);
                    }
                    else if (lastMove.x == 0 && lastMove.y == 1)
                    {
                        LanzarShuriken("arriba", arribaDerechalimite);
                    }
                    else if (lastMove.x == 0 && lastMove.y == -1)
                    {
                        LanzarShuriken("abajo", abajoIzquierdalimite);
                    }
                }
                else if (GameManager.instance.playerStats.itemK == "Wakizashi" && cdWakiCom && GameManager.instance.numDeItems[1] > 0) {
                    Debug.Log("Utiliza Wakizashi");
                    WakisazhiActivo();
                }
            }
            //verificacion de tecla presionada L 
            if (Input.GetKeyDown(KeyCode.L))
            {
                btnLPress = true;
                if (GameManager.instance.playerStats.itemL == "Espada Dragon")
                {
                    AtaqueEspada();
                }
                else if (GameManager.instance.playerStats.itemL == "Arco" && cdArcoCom && GameManager.instance.numDeItems[4] > 0)
                {
                    GameManager.instance.RemoveItem("Arco");
                    DireccionDañoAtaqueArco();
                    arcoInstancia = false;
                }
                else if (GameManager.instance.playerStats.itemL == "Bomba" && GameManager.instance.numDeItems[3] > 0)
                {
                    GameManager.instance.RemoveItem("Bomba");
                    if (lastMove.x == 1 && lastMove.y == 0)
                    {
                        AtaqueBomba("derecha");
                    }
                    else if (lastMove.x == -1 && lastMove.y == 0)
                    {
                        AtaqueBomba("izquierda");
                    }
                    else if (lastMove.x == 0 && lastMove.y == 1)
                    {
                        AtaqueBomba("arriba");
                    }
                    else if (lastMove.x == 0 && lastMove.y == -1)
                    {
                        AtaqueBomba("abajo");

                    }
                }
                else if (GameManager.instance.playerStats.itemK == "Shuriken" && cdShuCom && GameManager.instance.numDeItems[5] > 0)
                {
                    GameManager.instance.RemoveItem("Shuriken");
                    if (lastMove.x == 1 && lastMove.y == 0)
                    {
                        LanzarShuriken("derecha", arribaDerechalimite);
                    }
                    else if (lastMove.x == -1 && lastMove.y == 0)
                    {
                        LanzarShuriken("izquierda", abajoIzquierdalimite);
                    }
                    else if (lastMove.x == 0 && lastMove.y == 1)
                    {
                        LanzarShuriken("arriba", arribaDerechalimite);
                    }
                    else if (lastMove.x == 0 && lastMove.y == -1)
                    {
                        LanzarShuriken("abajo", abajoIzquierdalimite);
                    }
                }
                else if (GameManager.instance.playerStats.itemL == "Wakizashi" && cdWakiCom && GameManager.instance.numDeItems[1] > 0)
                {
                    WakisazhiActivo();
                }
            }
        }

        //------------------------------------------CONTADORES----------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------contador Espada-----------------------------------------------------------------------------------------------------------------------------
        if (atackTimeCounter > 0) {
            atackTimeCounter -= Time.deltaTime;
        }
        if (atackTimeCounter <= 0 && contadorTiemWaki <=0 && contadorTAnimShu <= 0 && !arcoActivo) {
            atacando = false;
            animator.SetBool("AtaqueEspada", false);
        }
        //----------------------------------------contador Wakizashi--------------------------------------------------------------------------------------------------------------------------
        if (contadorTiemWaki > 0) {
            contadorTiemWaki -= Time.deltaTime;
        }
        // devuelve al personaje la habilidad de caminar y reestablece los enfriamientos
        if(contadorTiemWaki <= 0 && atackTimeCounter <= 0 && contadorTAnimShu <= 0 && !arcoActivo)
        {            
            if (cdWakiCom && btnKPress && wakizashiActivo){
                btnKPress = false;
                enfriamientoObjetos.ActivarCdrWaki();
            }else if (cdWakiCom && btnLPress && wakizashiActivo){   
                btnLPress = false;
                enfriamientoObjetos.ActivarCdrWaki();
            }
            PHManager.invulnerable = false;
            atacando = false;
            wakizashiActivo = false;
            animator.SetBool("WakizashiActivo", wakizashiActivo);            
        }
        //----------------------------------------contador Shuriken---------------------------------------------------------------------------------------------------------------------------
        if (contadorTAnimShu > 0) {
            contadorTAnimShu -= Time.deltaTime;
        }
        // devuelve al personaje la habilidad de caminar y reestablece los enfriamientos
        if (contadorTAnimShu <= 0 && atackTimeCounter <= 0 && contadorTiemWaki <= 0 && !arcoActivo) {
            if (btnKPress && shurikenActivo && cdShuCom) {
                btnKPress = false;
                enfriamientoObjetos.ActivarCdrShuriken();
            } else if(btnLPress && shurikenActivo && cdShuCom) {
                btnLPress = false;
                enfriamientoObjetos.ActivarCdrShuriken();
            }
            atacando = false;
            shurikenActivo = false;
            animator.SetBool("ShurikenActivo", shurikenActivo);
        }
        // ----------------------------------------contador para Arco en K-----------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.K) && GameManager.instance.playerStats.itemK == "Arco" && arcoActivo) {
            playerRigidBody.velocity = Vector2.zero;
            arcoCargado = true;
            animator.SetBool("ArcoActivo", arcoActivo);
            animator.SetBool("ArcoCargado", arcoCargado);        
        }        
        if (Input.GetKeyUp(KeyCode.K) && GameManager.instance.playerStats.itemK == "Arco" && arcoActivo) {
            arcoCargado = false;
            animator.SetBool("ArcoCargado", arcoCargado);
            StartCoroutine(DispararArcoP2(tAnimFlecha, direccionFlecha, dirmovFlecha, damageArco));
            enfriamientoObjetos.ActivarCdrArco();
        }

        if (Input.GetKeyDown(KeyCode.L) && GameManager.instance.playerStats.itemL == "Arco" && arcoActivo){
            playerRigidBody.velocity = Vector2.zero;
            arcoCargado = true;
            animator.SetBool("ArcoActivo", arcoActivo);
            animator.SetBool("ArcoCargado", arcoCargado);
        }if (Input.GetKeyUp(KeyCode.L) && GameManager.instance.playerStats.itemL == "Arco" && arcoActivo)
        {
            arcoCargado = false;
            animator.SetBool("ArcoCargado", arcoCargado);
            StartCoroutine(DispararArcoP2(tAnimFlecha, direccionFlecha, dirmovFlecha, damageArco));
            enfriamientoObjetos.ActivarCdrArco();
        }
        //verificar si pasaron 2 segundo para aumentar daño
        if (contadorCargaArco > 0)
        {
            contadorCargaArco -= Time.deltaTime;
            dañoArcoX2 = false;
            Debug.Log("segundos faltantes " + contadorCargaArco);
        }
        else if (contadorCargaArco <= 0 && !dañoArcoX2)
        {
            dañoArcoX2 = true;
            damageArco += damageArco;
        }
        //valores de animacion de movimiento----------------------------------------------------------------------------------------------------------------------------------
        animator.SetFloat("MovX", playerRigidBody.velocity.x);
        animator.SetFloat("MovY", playerRigidBody.velocity.y);
        animator.SetBool("RaidenMoviendose", playermoving);
        animator.SetFloat("UltimoX", lastMove.x);
        animator.SetFloat("UltimoY", lastMove.y);
        //evitar que el personaje salga del mapa----------------------------------------------------------------------------------------------------------------------------------
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, abajoIzquierdalimite.x, arribaDerechalimite.x), Mathf.Clamp(transform.position.y, abajoIzquierdalimite.y, arribaDerechalimite.y), transform.position.z);
    }

    //funcion utilizada para determinar los limites dentro del mapa
    public void SetBounds(Vector3 abizq, Vector3 arrder) {
        abajoIzquierdalimite = abizq + new Vector3(.5f, .5f, 0);
        arribaDerechalimite = arrder; new Vector3(-3f, -5f, 0);
    }

    public void AtaqueEspada() {
        atackTimeCounter = atackTime;
        atacando = true;
        playerRigidBody.velocity = Vector2.zero;
        animator.SetBool("AtaqueEspada", true);
    }

    public void DireccionDañoAtaqueArco() {
        damageArco = GameManager.instance.playerStats.dañoArco;
        if (lastMove.x == 1 && lastMove.y == 0)
        {
            atacando = true;
            arcoActivo = true;
            contadorCargaArco = cargaArco;
            direccionFlecha = "derecha";
            dirmovFlecha = arribaDerechalimite;
        }
        else if (lastMove.x == -1 && lastMove.y == 0)
        {
            atacando = true;
            arcoActivo = true;
            contadorCargaArco = cargaArco;
            direccionFlecha = "izquierda";
            dirmovFlecha = abajoIzquierdalimite;
        }
        else if (lastMove.x == 0 && lastMove.y == 1)
        {
            atacando = true;
            arcoActivo = true;
            contadorCargaArco = cargaArco;
            direccionFlecha = "arriba";
            dirmovFlecha = arribaDerechalimite;
        }
        else if (lastMove.x == 0 && lastMove.y == -1)
        {
            atacando = true;
            arcoActivo = true;
            contadorCargaArco = cargaArco;
            direccionFlecha = "abajo";
            dirmovFlecha = abajoIzquierdalimite;
        }
    }

    public void AtaqueBomba(string direccion) {
        atackTimeCounter = atackTime;
        atacando = true;
        playerRigidBody.velocity = Vector2.zero;
        instanciaBomba = Instantiate(bomba, transform.position, transform.rotation);
        instanciaBomba.GetComponent<BombaControlador>().SeleccionDireccion(direccion);
    }

    public void WakisazhiActivo() {
        contadorTiemWaki = tiempoWaki;
        PHManager.invulnerable = true;
        atacando = true;
        wakizashiActivo = true;
        playerRigidBody.velocity = Vector2.zero;
        animator.SetBool("WakizashiActivo", wakizashiActivo);
    }

    public void LanzarShuriken(string direccion, Vector3 dirMov) {
        contadorTAnimShu = tAnimShu;
        atacando = true;
        shurikenActivo = true;
        playerRigidBody.velocity = Vector2.zero;
        animator.SetBool("ShurikenActivo", shurikenActivo);
        instanciaShu = Instantiate(shuriken, transform.position, transform.rotation);
        instanciaShu.GetComponent<ShurikenControlador>().GetValores(direccion, dirMov);
    }

    //coorutina que reestablece el enfriamiento del shuriken
    IEnumerator EnfriamientoShuriken(float segundos) {
        if (btnKPress || btnLPress)
        {
            cdShuCom = false;
            yield return new WaitForSeconds(segundos);
            cdShuCom = true;
            btnKPress = false;
            btnLPress = false;
        }
    }
    //coorutina que reestablece el enfriamiento del arco
    IEnumerator EnfriamientoArco(float segundos) {
        if (btnKPress || btnLPress)
        {
            cdArcoCom = false;
            yield return new WaitForSeconds(segundos);
            cdArcoCom = true;
            btnKPress = false;
            btnLPress = false;
        }
    }
    

    IEnumerator DispararArcoP2(float tiempoAnim, string direccionFlechaStr, Vector3 dirMovV3, int dañoArcoAhora)
    {
        yield return new WaitForSeconds(tiempoAnim);
        arcoActivo = false;
        animator.SetBool("ArcoActivo", arcoActivo);
        playerRigidBody.velocity = Vector2.zero;
        if (!arcoInstancia)
        {
            arcoInstancia = true;
            instanciaArco = Instantiate(arco, transform.position, transform.rotation);
            instanciaArco.GetComponent<ArcoControlador>().SetValores(direccionFlechaStr, dirMovV3, dañoArcoAhora);
        }
    }

}
