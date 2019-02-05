using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeVeneno : MonoBehaviour
{
    public float velocidadProyectil;
    public int damage = -1, totalDamage = 4;
    private int contadorD = 0;
    public float tiempoD = 1f; 
    private float contadortiempoD;
    private GameObject player;
    private Rigidbody2D orbeFRB2D;
    private Vector3 objetivo, direccion;

    // Start is called before the first frame update
    void Start()
    {
        totalDamage = GameManager.instance.magoStats.dañoTotalVeneno;
        player = GameObject.FindGameObjectWithTag("Player");
        orbeFRB2D = GetComponent<Rigidbody2D>();
        contadortiempoD = tiempoD;
        if (player != null)
        {
            objetivo = player.transform.position;
            direccion = (objetivo - transform.position).normalized;
        }
    }

    void Update()
    {
            
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
            collision.gameObject.GetComponent<PlayerHealtManager>().ProgressiveDammage(damage, totalDamage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
