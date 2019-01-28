using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsscentialsLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }
        if (PlayerControlador.instance == null) {
            PlayerControlador clon = Instantiate(player).GetComponent<PlayerControlador>();
            PlayerControlador.instance = clon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
