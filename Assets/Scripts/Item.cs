using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string descripcion;
    public Sprite itemSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //función que hace que el item se equipe en la letra K o L
    public void Use(int btnNum) {
        CharStats playerStat = GameManager.instance.playerStats;
        string itemProv;
        //boton K
        if (btnNum == 0) {
            if (playerStat.itemL == itemName) {
                itemProv = playerStat.itemL;
                playerStat.itemL = playerStat.itemK;
                playerStat.itemK = itemProv;
            }
            else {
                playerStat.itemK = itemName;
            }       

        }
        //boton L
        if (btnNum == 1){
            if (playerStat.itemK == itemName) {
                itemProv = playerStat.itemK;
                playerStat.itemK = playerStat.itemL;
                playerStat.itemL = itemProv;
            }else{
                playerStat.itemL = itemName;
            }
        }        
    }
}
