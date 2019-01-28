 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool GmExist;
    public CharStats playerStats;
    public bool gameMenuOpen, dialogoActivo, fadingEntreAreas;
    //items
    public string[] itemStock;
    public int[] numDeItems;
    public Item[] referenciaItem;

    // Start is called before the first frame update
    void Start()
    {
        if (!GmExist)
        {
            GmExist = true;
            DontDestroyOnLoad(transform.gameObject);
            instance = this;
            // UpdateVida();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogoActivo || fadingEntreAreas) {
            PlayerControlador.instance.puedoMoverme = false;
        } else {
            PlayerControlador.instance.puedoMoverme = true;
        }

        /*if (Input.GetKeyDown(KeyCode.Y)) {
            AddItem("Bomba");
            AddItem("Tu titulo de Tecnologo ");
            RemoveItem("Arco");
            RemoveItem("Tu fealdad prro ");
        }*/

    }

    //funcion que permite obtener la información de un item
    public Item GetItemDetail(string itemBuscado) {
        for (int i = 0; i < referenciaItem.Length; i++) {
            if (referenciaItem[i].itemName == itemBuscado) {
                return referenciaItem[i];
            }
        }
        return null;
    }

    //funcion utilizada para agregar items en el inventario, checando si existen o no
    public void AddItem(string itemToAdd) {
        int newItemPos = 0;
        bool foundSpace = false;
        //checa si hay espacios disponibles para el item
        for (int i = 0; i < itemStock.Length; i++) {
            if (itemStock[i] == "" || itemStock[i] == itemToAdd) {
                newItemPos = i;
                i = itemStock.Length;
                foundSpace = true;
            }
        }
        // verifica si el item que se desea añadir existe
        if (foundSpace) {
            bool itemExist = false;
            for (int i = 0; i < referenciaItem.Length; i++) {
                if (referenciaItem[i].itemName == itemToAdd) {
                    itemExist = true;
                    i = referenciaItem.Length;
                }
            }
            if (itemExist) {
                itemStock[newItemPos] = itemToAdd;
                numDeItems[newItemPos]++;
            }else{
                Debug.LogError(itemToAdd + "No existe!");
            }
        }
        GameMenu.instance.ShowItems();
    }

    //función para sacar un item del inventario
    public void RemoveItem(string itemToRemove) {
        bool foundItem = false;
        int itemPos = 0;
        for (int i = 0; i < itemStock.Length; i++) {
            if (itemStock[i] == itemToRemove) {
                foundItem = true;
                itemPos = i;
                i = itemStock.Length;
            }
        }
        if (foundItem) {
            if (numDeItems[itemPos] > 0) {
                numDeItems[itemPos]--;
            }
        }else {
            Debug.LogError("No se puede encontrar " + itemToRemove);
        }
    }

}
