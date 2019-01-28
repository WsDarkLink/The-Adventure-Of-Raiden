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
    public bool puedoMoverme;
    private static bool playerExist;

    //variables para atacar
    public float atackTime;
    private float atackTimeCounter;
    private bool atacando;

    //instancia del mismo GO
    public static PlayerControlador instance;
    //variable utilizada para cambiar de zonas
    public string areaTransicion;

    //variables para no salir de mapa
    private Vector3 abajoIzquierdalimite;
    private Vector3 arribaDerechalimite;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        puedoMoverme = true;
        //evitar que el jugador se duplique y/o desaparezca
        /*if (instance == null)
        {
            instance = this;
        }
        else {
            if (instance != this){
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);*/
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

        if (!atacando){
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (moveInput != Vector2.zero)
            {
                playerRigidBody.velocity = new Vector2(moveInput.x * velocidadMovimiento, moveInput.y * velocidadMovimiento);
                playermoving = true;
                lastMove = moveInput;
            }else {
                playerRigidBody.velocity = Vector2.zero;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                atackTimeCounter = atackTime;
                atacando = true;
                playerRigidBody.velocity = Vector2.zero;
                animator.SetBool("AtaqueEspada", true);
            }
        }
        if (atackTimeCounter > 0) {
            atackTimeCounter -= Time.deltaTime;
        }
        if (atackTimeCounter <= 0) {
            atacando = false;
            animator.SetBool("AtaqueEspada", false);
        }

        animator.SetFloat("MovX", playerRigidBody.velocity.x);
        animator.SetFloat("MovY", playerRigidBody.velocity.y);
        animator.SetBool("RaidenMoviendose", playermoving);
        animator.SetFloat("UltimoX", lastMove.x);
        animator.SetFloat("UltimoY", lastMove.y);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, abajoIzquierdalimite.x, arribaDerechalimite.x), Mathf.Clamp(transform.position.y, abajoIzquierdalimite.y, arribaDerechalimite.y), transform.position.z);
    }

    public void SetBounds(Vector3 abizq, Vector3 arrder) {
        abajoIzquierdalimite = abizq + new Vector3(.5f, .5f, 0);
        arribaDerechalimite = arrder; new Vector3(-3f, -5f, 0);
    }

}
