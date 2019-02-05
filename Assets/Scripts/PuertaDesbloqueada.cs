using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaDesbloqueada : MonoBehaviour
{
    private BoxCollider2D BoxC2D;
    private Animator animator;
    private bool desbloqueada;


    // Start is called before the first frame update
    void Start()
    {
        BoxC2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Desbloqueada", desbloqueada);        
    }

    public void Desbloqueado() {
        if (desbloqueada) {
            desbloqueada = false;
            BoxC2D.enabled = true;
        }
        else{
            desbloqueada = true;
            BoxC2D.enabled = false;
        }
    }
}
