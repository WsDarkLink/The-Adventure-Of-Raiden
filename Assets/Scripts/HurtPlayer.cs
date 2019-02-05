using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = GameManager.instance.meleStats.dañoMele;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

         if (collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<PlayerHealtManager>().HurtPlayer(damage);
         }

    }

}
