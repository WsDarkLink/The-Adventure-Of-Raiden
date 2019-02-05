using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeFuego : MonoBehaviour
{
    public float velocidadProyectil;
    public int damage = -2;
    private GameObject player;
    private Rigidbody2D orbeFRB2D;
    private Vector3 objetivo, direccion;
    float segundos = .2f;
    // Start is called before the first frame update
    void Start()
    {
        damage = GameManager.instance.magoStats.dañoOrbeFuego;
        player = GameObject.FindGameObjectWithTag("Player");
        orbeFRB2D = GetComponent<Rigidbody2D>();
        if (player != null) {
            objetivo = player.transform.position;
            direccion = (objetivo - transform.position).normalized;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objetivo != Vector3.zero) {
            orbeFRB2D.MovePosition(transform.position + (direccion * velocidadProyectil) * Time.deltaTime);
        }        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            StartCoroutine(DestruirObjeto(segundos));
            collision.gameObject.GetComponent<PlayerHealtManager>().HurtPlayer(damage);
            
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator DestruirObjeto(float segundos) {
        yield return new WaitForSeconds(segundos);
        Destroy(gameObject);
    }

}
