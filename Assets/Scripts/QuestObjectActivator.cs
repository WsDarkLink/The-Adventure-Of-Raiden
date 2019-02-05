using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject objetoParaActivar;

    public string questToCheck;

    public bool activarIfCompleto;
    private bool checkInicial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkInicial) {
            checkInicial = true;
            CheckTerminacionQuest();
        }
    }

    public void CheckTerminacionQuest() {
        if (QuestManager.instance.CheckIfComplete(questToCheck)) {
            objetoParaActivar.SetActive(activarIfCompleto);
        }
    }

}
