using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaControlador : MonoBehaviour
{
    public int damageDeal, damagePlayer = -4;
    public GameObject damageNumber;
    public BoxCollider2D boxCol2D;
    public Animator animator;
    private float segundos = 2f, contadorSeg;
    public float segAnimFin;
    private bool timeOut, noDaño;
    // Start is called before the first frame update
    void Start()
    {
        boxCol2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        damageDeal = GameManager.instance.playerStats.dañoBomba;
        contadorSeg = segundos;
        animator.SetBool("TimeOut", timeOut);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (contadorSeg >0)
        {
            contadorSeg -= Time.deltaTime;
            
        }
        else {
            timeOut = true;
            boxCol2D.isTrigger = true;
            animator.SetBool("TimeOut", timeOut);
            StartCoroutine(TiempoAnim(segAnimFin));
        }
    }

    public void SeleccionDireccion(string direccion) {
        if (direccion == "derecha")
        {
            transform.position += new Vector3(1, 0, 0);
        }
        else if (direccion == "izquierda")
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        else if (direccion == "arriba")
        {
            //que onda que pez
        }
        else if (direccion == "abajo") {
            transform.position += new Vector3(0, -1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ataques" && !timeOut) {
            
            timeOut = true;
            boxCol2D.isTrigger = true;
            animator.SetBool("TimeOut", timeOut);
            StartCoroutine(TiempoAnim(segAnimFin));
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (timeOut)
        {
            if (collision.gameObject.tag == "Enemy" && timeOut)
            {
                collision.gameObject.GetComponent<EnemyHealtManager>().HurtEnemy(damageDeal);
                var clone = (GameObject)Instantiate(damageNumber, transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<NumerosFlotando>().damageNum = damageDeal;         
            }
            if (collision.gameObject.tag == "Player" && timeOut && !noDaño) {
                noDaño = true;
                PlayerControlador.instance.GetComponent<PlayerHealtManager>().HurtPlayer(damagePlayer);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator TiempoAnim(float segundos) {
        yield return new WaitForSeconds(segAnimFin);
        Destroy(gameObject);
    }

}
