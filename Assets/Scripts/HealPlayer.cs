using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public Item item;
    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        if (item.itemName == "Capsula de Curacion") {
            amount = 4;
        }
        else {
            amount = 8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.K)) {
                collision.gameObject.GetComponent<PlayerHealtManager>().HealPlayer(amount);
                Destroy(gameObject);
            }
        }
    }

}
