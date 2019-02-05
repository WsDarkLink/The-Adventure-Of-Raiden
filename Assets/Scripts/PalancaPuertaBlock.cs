using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaPuertaBlock : MonoBehaviour
{

    private Animator animator;
    private bool desbloqueado;
    public GameObject puertaPAbrir;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (!desbloqueado)
                {
                    desbloqueado = true;
                    animator.SetBool("Desbloqueado", desbloqueado);
                    puertaPAbrir.GetComponent<PuertaDesbloqueada>().Desbloqueado();
                }
                else
                {
                    desbloqueado = false;
                    animator.SetBool("Desbloqueado", desbloqueado);
                    puertaPAbrir.GetComponent<PuertaDesbloqueada>().Desbloqueado();
                }
            }
        }
    }

}
