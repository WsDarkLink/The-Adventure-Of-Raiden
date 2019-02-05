﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string[] questMarkerNames;
    public bool[] questMarkersComplete;

    public static QuestManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questMarkersComplete = new bool[questMarkerNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log(CheckIfComplete("Ponte bien pedo"));
            MarcarQuestCompleto("Ponte bien pedo");
            MarcarQuestIncompleto("Termina el sistema");
        }
    }

    public int GetQuestNumber(string questToFind) {

        for (int i = 0; i < questMarkerNames.Length; i++) {
            if (questMarkerNames[i] == questToFind) {
                return i;
            }
        }
        Debug.LogError("Quest " + questToFind + " No existe ALV");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        if (GetQuestNumber(questToCheck) != 0) {
            return questMarkersComplete[GetQuestNumber(questToCheck)];
        }
        return false;
    }

    public void MarcarQuestCompleto(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = true;
        GameManager.instance.playerStats.AddLevel();
        GameManager.instance.magoStats.LevelUp();
        GameManager.instance.arqueroStats.LevelUp();
        GameManager.instance.meleStats.LevelUp();
        UpdateLocalQuestObject();
    }

    public void MarcarQuestIncompleto(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObject();
    }

    public void UpdateLocalQuestObject() {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if (questObjects.Length > 0) {
            for (int i = 0; i < questObjects.Length; i++) {
                questObjects[i].CheckTerminacionQuest();
            }
        }
    }

    public void SaveQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkersComplete[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            int valueToSet = 0;
            if (PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
            }

            if (valueToSet == 0)
            {
                questMarkersComplete[i] = false;
            }
            else
            {
                questMarkersComplete[i] = true;
            }
        }
    }

}
