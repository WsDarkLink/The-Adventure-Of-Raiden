using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dBox;
    public GameObject nBox;
    public Text dtex;
    public Text nombrePer;

    public bool dialogoActivo;
    public string[] lineasDeDialogo;
    public int lineaActual;
    public string nombre;
    private PlayerControlador thePlayer;


    void Start()
    {

        thePlayer = FindObjectOfType<PlayerControlador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogoActivo && Input.GetKeyDown(KeyCode.Space)) {
            /*dBox.SetActive(false);
            dialogoActivo = false;*/
            lineaActual++;

            if (lineaActual >= lineasDeDialogo.Length) {
                dBox.SetActive(false);
                dialogoActivo = false;
                lineaActual = 0;
                GameManager.instance.dialogoActivo = false;
                //thePlayer.puedoMoverme = true;
            }
        }
        dtex.text = lineasDeDialogo[lineaActual];
    }

    public void MostrarDialogos()
    {
        GameManager.instance.dialogoActivo = true;
        //thePlayer.puedoMoverme = false;
        dialogoActivo = true;
        dBox.SetActive(true);
        nombrePer.text = nombre;
        if (nombre == "-") {
            nBox.SetActive(false);
        }
    }

}
