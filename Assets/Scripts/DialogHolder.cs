using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour
{
    // Start is called before the first frame update
    private DialogoManager dManager;
    public string[] lineasDeDialogo;
    public string nombre;

    void Start()
    {
        dManager = FindObjectOfType<DialogoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (Input.GetKeyUp(KeyCode.K)) {
                //dManager.MostrarDialogo(dialogo);
                if (!dManager.dialogoActivo) {
                    dManager.lineasDeDialogo = lineasDeDialogo;
                    dManager.nombre = nombre;
                    dManager.lineaActual = 0;
                    dManager.MostrarDialogos();
                }
                /*if (transform.parent.GetComponent<NPCMovement>() != null) {
                    transform.parent.GetComponent<NPCMovement>().puedeMoverse = false;
                }*/
            }
        }
    }

}
