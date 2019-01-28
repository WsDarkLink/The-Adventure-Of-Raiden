using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBtn : MonoBehaviour
{
    public Image btnImage;
    public Text amount;
    public int btnValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //manda la información del item seleccionado utilizando valores guardados en el GameManager
    public void Press() {
        if (GameManager.instance.itemStock[btnValue] != "") {
            GameMenu.instance.SelectItem(GameManager.instance.GetItemDetail(GameManager.instance.itemStock[btnValue]));
        }
    }

}
