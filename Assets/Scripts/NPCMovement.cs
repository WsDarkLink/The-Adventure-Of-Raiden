using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float velocidadMovimiento;
    public bool caminando;
    public float tiempoCaminando;
    private float contadorCaminando;
    public float tiempoEspera;
    private float contadorEspera;

    private int direccionCaminando;

    private Rigidbody2D npcRigidBody;

    public Collider2D walkZone;
    private Vector2 minWalkZone;
    private Vector2 maxWalkZone;
    private bool hasWalkZone;
    public bool puedeMoverse;
    private DialogoManager dManager;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        npcRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dManager = FindObjectOfType<DialogoManager>();
        contadorCaminando = tiempoCaminando;
        contadorEspera = tiempoEspera;

        ElegirDireccion();
        if(walkZone != null) { 
            minWalkZone = walkZone.bounds.min;
            maxWalkZone = walkZone.bounds.max;
            hasWalkZone = true;
        }
        puedeMoverse = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!dManager.dialogoActivo) {
            puedeMoverse = true;
        }

        if (!puedeMoverse) {
            npcRigidBody.velocity = Vector2.zero;
            return;
        }

        if (caminando) {
            contadorCaminando -= Time.deltaTime;
            

            switch (direccionCaminando) {
                case 0:
                    npcRigidBody.velocity = new Vector2(0, velocidadMovimiento);
                   
                    if (hasWalkZone && transform.position.y > maxWalkZone.y) {
                        caminando = false;
                        contadorEspera = tiempoEspera;
                        
                    }
                    break;
                case 1:
                    npcRigidBody.velocity = new Vector2(velocidadMovimiento, 0);
                    
                    if (hasWalkZone && transform.position.x > maxWalkZone.x)
                    {
                        caminando = false;
                        contadorEspera = tiempoEspera;
                       
                    }
                    break;      
                case 2:
                    npcRigidBody.velocity = new Vector2(0, -velocidadMovimiento);
                   
                    if (hasWalkZone && transform.position.y < minWalkZone.y)
                    {
                        caminando = false;
                        contadorEspera = tiempoEspera;
                       
                    }
                    break;
                case 3:
                    npcRigidBody.velocity = new Vector2(-velocidadMovimiento, 0);
                  
                    if (hasWalkZone && transform.position.x < minWalkZone.x)
                    {
                        caminando = false;
                        contadorEspera = tiempoEspera;
                        
                    }
                    break;
            }
            if (contadorCaminando < 0){
                caminando = false;
                
                contadorEspera = tiempoEspera;
            }
        }
        else {
            contadorEspera -= Time.deltaTime;
            npcRigidBody.velocity = Vector2.zero;
            if (contadorEspera < 0) {
                ElegirDireccion();
            }
        }
    }

    void ElegirDireccion() {
        direccionCaminando = Random.Range(0, 4);
        caminando = true;
        contadorCaminando = tiempoCaminando;
        
    }

}
