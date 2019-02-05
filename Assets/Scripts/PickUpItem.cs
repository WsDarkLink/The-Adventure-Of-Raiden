using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private bool puedeAgarrarlo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.J)) {
                GameManager.instance.AddItem(GetComponent<Item>().itemName);
                Destroy(gameObject);
            }
        }
    }

}
