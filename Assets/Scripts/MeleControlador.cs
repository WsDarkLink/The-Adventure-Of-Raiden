using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeleControlador : MonoBehaviour
{
    public float tiempoEntreMov;
    public float tiempoParaMov;
    public float velocidadMovimiento;

    public float waitToReload;
    private bool reloading;
    private GameObject thePlayer;

    private Animator animator;
    private float tiempoEntreMovContador;
    private float tiempoParaMovContador;
    private Rigidbody2D miRigidBody;
    private bool moviendose;
    private float ultimoX;
    private float ultimoY;
    private Vector3 direccionMov;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        miRigidBody = GetComponent<Rigidbody2D>();
        //tiempoEntreMovContador = tiempoEntreMov;
        //tiempoParaMovContador = tiempoParaMov;
        tiempoEntreMovContador = Random.Range(tiempoEntreMov * .75f, tiempoEntreMov * 1.25f);
        tiempoParaMovContador = Random.Range(tiempoParaMov * .75f, tiempoEntreMov * 1.25f);
    }

    // Update is called once per frame
    void Update()
    {

        if (moviendose){
            tiempoParaMovContador -= Time.deltaTime;
            miRigidBody.velocity = direccionMov;
            if (tiempoParaMovContador < 0f) {
                moviendose = false;
                //tiempoEntreMovContador = tiempoEntreMov;
                animator.SetBool("MeleMoviendose", false);
                tiempoEntreMovContador = Random.Range(tiempoEntreMov * .75f, tiempoEntreMov * 1.25f);
                
            }

        } else {
            miRigidBody.velocity = Vector2.zero;
            tiempoEntreMovContador -= Time.deltaTime;
            if (tiempoEntreMovContador < 0f) {
                moviendose = true;
                //tiempoParaMovContador = tiempoParaMov;
                tiempoParaMovContador = Random.Range(tiempoParaMov * .75f, tiempoEntreMov * 1.25f);
                ultimoX = Random.Range(-1f, 1f);
                ultimoY = Random.Range(-1f, 1f);
                animator.SetBool("MeleMoviendose", true);
                direccionMov = new Vector3(ultimoX * velocidadMovimiento, ultimoY * velocidadMovimiento, 0f);
            }
        }

        if (reloading) {
            waitToReload -= Time.deltaTime;
            if (waitToReload < 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                thePlayer.SetActive(true);
            }
        } 

    }

    void OnCollisionEnter2D(Collision2D collision){

       /* if (collision.gameObject.name == "Player"){
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            reloading = true;
            thePlayer = collision.gameObject;
        }*/

    }


}
