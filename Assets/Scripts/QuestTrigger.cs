using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public int numeroQuest;

    public bool startQuest;
    public bool endQuest;

    private QuestManager QManager;

    void Start()
    {
        QManager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            if (!QManager.questCompletado[numeroQuest]) {
                if (startQuest && !QManager.quests[numeroQuest].gameObject.activeSelf) {
                    QManager.quests[numeroQuest].gameObject.SetActive(true);
                    QManager.quests[numeroQuest].StartQuest();
                }

                if (endQuest && QManager.quests[numeroQuest].gameObject.activeSelf) {
                    QManager.quests[numeroQuest].EndQuest();
                }
            }
        }    
    }

}
