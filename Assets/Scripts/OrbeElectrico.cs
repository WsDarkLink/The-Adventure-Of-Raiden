using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeElectrico : MonoBehaviour
{
    public float velocidadProyectil;
    public int damage = -1;
    public float tiempoParalizado = 1.5f;
    private GameObject player;
    private Rigidbody2D orbeFRB2D;
    private Vector3 objetivo, direccion;

    // Start is called before the first frame update
    void Start()
    {
        damage = GameManager.instance.magoStats.dañoOrbeElectrico;
        tiempoParalizado = GameManager.instance.magoStats.tiempoParalisis;
        player = GameObject.FindGameObjectWithTag("Player");
        orbeFRB2D = GetComponent<Rigidbody2D>();
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
            orbeFRB2D.MovePosition(transform.position + (direccion * velocidadProyectil) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealtManager>().ParalizarJugador(tiempoParalizado, damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
