﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float radioVision;
    public float radioAtaque;
    public float velocidadMovimiento;

    private Vector3 posicionInicial;

    private Animator animator;
    private Rigidbody2D enemyRB2D;

    public bool puedeAtacar, disponeAtaque;
    public float tiempoAtaque = .35f, cdAtaque = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        posicionInicial = transform.position;
        animator = GetComponent<Animator>();
        enemyRB2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objetivo = posicionInicial;
        Vector3 mamada = PlayerControlador.instance.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mamada, radioVision, 1 << LayerMask.NameToLayer("Default"));
        Vector3 lineaRay = transform.TransformDirection(PlayerControlador.instance.transform.position - transform.position);
        Debug.DrawRay(transform.position, lineaRay, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {                
                objetivo = PlayerControlador.instance.transform.position;
            }
        }

        float distancia = Vector3.Distance(objetivo, transform.position);
        Vector3 direccion = (objetivo - transform.position).normalized;

        if (objetivo != posicionInicial && distancia < radioAtaque)
        {
            animator.SetFloat("MovX", direccion.x);
            animator.SetFloat("MovY", direccion.y);
            
            if (!puedeAtacar && !disponeAtaque) {
                StartCoroutine(Atacar(tiempoAtaque, cdAtaque));
            }

        }
        else
        {
            enemyRB2D.MovePosition(transform.position + direccion * velocidadMovimiento * Time.deltaTime);
            animator.speed = 1;
            animator.SetFloat("MovX", direccion.x);
            animator.SetFloat("MovY", direccion.y);
            animator.SetBool("Moviendose", true);
        }

        if (objetivo == posicionInicial && distancia < 0.02)
        {
            transform.position = posicionInicial;
            animator.SetBool("Moviendose", false);
        }

    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVision);
        Gizmos.DrawWireSphere(transform.position, radioAtaque);

    }

    IEnumerator Atacar(float segAnimAtack, float segundoEspera) {
        puedeAtacar = true;
        disponeAtaque = true;
        animator.SetBool("Atacando", puedeAtacar);
        yield return new WaitForSeconds(segAnimAtack);
        puedeAtacar = false;
        animator.SetBool("Atacando", puedeAtacar);
        yield return new WaitForSeconds(segundoEspera);
        disponeAtaque = false;
    }

}