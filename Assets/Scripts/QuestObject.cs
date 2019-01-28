using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    // Start is called before the first frame update
    public int questNumber;

    public QuestManager QManager;
    public string startText;
    public string endText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuest() {
        QManager.ShowQuestText(startText);
    }

    public void EndQuest() {
        QManager.ShowQuestText(endText);
        QManager.questCompletado[questNumber] = true;
        gameObject.SetActive(false);
    }

}
