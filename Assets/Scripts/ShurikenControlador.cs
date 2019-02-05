using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenControlador : MonoBehaviour
{
    public float velocidadProyectil;
    public int damageDeal;
    public GameObject damageNumber;
    private Rigidbody2D shurikenFRB2D;
    private Vector3 objetivo, direccionV;
    float segundos = .2f;

    // Start is called before the first frame update
    void Start()
    {
        shurikenFRB2D = GetComponent<Rigidbody2D>();
        damageDeal = GameManager.instance.playerStats.dañoShuriken;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {       
            shurikenFRB2D.MovePosition(transform.position + (direccionV * velocidadProyectil) * Time.deltaTime);       
    }

    public void GetValores(string direccion, Vector3 dirMov) {
        if (direccion == "derecha")
        {
            transform.position += new Vector3(1, 0, 0);
            objetivo = new Vector3 (2000,0,0) + transform.position + dirMov;
            direccionV = (objetivo - transform.position).normalized;
        }
        else if (direccion == "izquierda")
        {
            transform.position += new Vector3(-1, 0, 0);
            objetivo = new Vector3(-2000, 0, 0) + transform.position + dirMov;
            direccionV = (objetivo - transform.position).normalized;
        }
        else if (direccion == "arriba")
        {
            objetivo = new Vector3(0, 2000, 0) + transform.position + dirMov;
            direccionV = (objetivo - transform.position).normalized;
        }
        else if (direccion == "abajo") {
            transform.position += new Vector3(0, -1, 0); 
            objetivo = new Vector3(0, -2000, 0) + transform.position + dirMov;
            direccionV = (objetivo - transform.position).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealtManager>().HurtEnemy(damageDeal);
            var clone = (GameObject)Instantiate(damageNumber, transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<NumerosFlotando>().damageNum = damageDeal;
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
