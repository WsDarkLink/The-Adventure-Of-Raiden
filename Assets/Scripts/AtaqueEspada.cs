using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEspada : MonoBehaviour
{
    public int damageDeal;
    public GameObject damageNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<EnemyHealtManager>().HurtEnemy(damageDeal);
            var clone = (GameObject)Instantiate(damageNumber, transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<NumerosFlotando>().damageNum = damageDeal;
        }   
    }

}
