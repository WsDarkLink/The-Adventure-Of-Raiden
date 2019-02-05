﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    public string questToMark;
    public bool markComplete;
    public bool markOnEnter;
    private bool canMark;
    public bool deactivateOnMarking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMark && Input.GetKeyDown(KeyCode.J)) {
            canMark = false;
            MarkQuest();
        }    
    }

    public void MarkQuest() {
        if (markComplete) {
            QuestManager.instance.MarcarQuestCompleto(questToMark);
        }
        else{
            QuestManager.instance.MarcarQuestIncompleto(questToMark);
        }
        gameObject.SetActive(!deactivateOnMarking);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (markOnEnter){
                MarkQuest();
            }
            else {
                canMark = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canMark = false;
        }
    }

}
