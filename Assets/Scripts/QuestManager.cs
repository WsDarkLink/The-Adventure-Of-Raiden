using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public QuestObject[] quests;
    public bool[] questCompletado;
    public DialogoManager DManager;

    void Start()
    {
        questCompletado = new bool[quests.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestText(string questText) {
        DManager.lineasDeDialogo = new string[1];
        DManager.lineasDeDialogo[0] = questText;
        DManager.lineaActual = 0;
        DManager.MostrarDialogos();

    }

}
