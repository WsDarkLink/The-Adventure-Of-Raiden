using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreControlador : MonoBehaviour
{

    private Animator animator;
    private bool abierto;
    public Item itemToGive;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!abierto)
                {
                    abierto = true;
                    animator.SetBool("Abierto", abierto);
                    GameManager.instance.AddItem(itemToGive.itemName);
                }
            }
        }
    }
}
