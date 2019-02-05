using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaControlador : MonoBehaviour
{
    public float velocidadProyectil;
    public int damage = -2;
    private GameObject player;
    private Rigidbody2D flechaFRB2D;
    private Animator animator;
    private Vector3 objetivo, direccion;
    float segundos = .2f;
    // Start is called before the first frame update
    void Start()
    {
        damage = GameManager.instance.arqueroStats.dañoArquero;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        flechaFRB2D = GetComponent<Rigidbody2D>();
        if (player != null)
        {
            objetivo = player.transform.position;
            direccion = (objetivo - transform.position).normalized;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objetivo != Vector3.zero)
        {
            flechaFRB2D.MovePosition(transform.position + (direccion * velocidadProyectil) * Time.deltaTime);
            animator.SetFloat("MovX", direccion.x);
            animator.SetFloat("MovY", direccion.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealtManager>().HurtPlayer(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

  
}
