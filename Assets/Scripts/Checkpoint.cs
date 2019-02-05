using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private BoxCollider2D boxC2D;
    // Start is called before the first frame update
    void Start()
    {
        boxC2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            GameManager.instance.SaveData();
            QuestManager.instance.SaveQuestData();
            gameObject.SetActive(false);
        }
    }

}
