using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject[] ventanas;
    public ItemBtn[] itemBtns;
    public static GameMenu instance;
    public string selectetItem;

    public Item activeItem;
    public Text itemName, itemDescription;
    public GameObject itemBtnOptionMenu;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (menu.activeInHierarchy) {
                menu.SetActive(false);
                ventanas[0].SetActive(true);
                ventanas[1].SetActive(false);
                GameManager.instance.gameMenuOpen = false;
                itemBtnOptionMenu.SetActive(false);
            } else {
                menu.SetActive(true);
                GameManager.instance.gameMenuOpen = true;
                ShowItems();
            }
        }
    }
    //Muestra y oculta las ventanas de inventario y ajustes
    public void CambiarVentanas(int numVentana)
    {
        for (int i = 0; i < ventanas.Length; i++) {
            if (i == numVentana) {
                ventanas[i].SetActive(!ventanas[i].activeInHierarchy);
            }else{
                ventanas[i].SetActive(false);
            }
        }
        itemBtnOptionMenu.SetActive(false);
    }
    //Muestra los items que posee el jugador en pantalla
    public void ShowItems() {
        for (int i = 0; i < itemBtns.Length; i++) {
            itemBtns[i].btnValue = i;
            if (GameManager.instance.itemStock[i] != "") {
                itemBtns[i].btnImage.gameObject.SetActive(true);
                itemBtns[i].btnImage.sprite = GameManager.instance.GetItemDetail(GameManager.instance.itemStock[i]).itemSprite;
                itemBtns[i].amount.text = GameManager.instance.numDeItems[i].ToString();
            }else {
                itemBtns[i].btnImage.gameObject.SetActive(false);
                itemBtns[i].amount.text = "";
            }
        }
    }
    //Muestra la información del item en pantalla
    public void SelectItem(Item newItem) {
        activeItem = newItem;
        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.descripcion;
    }
    //abre el panel para elegir en que boton se va a equipar el item
    public void OpenItemBtnOption() {
        itemBtnOptionMenu.SetActive(true);
    }
    // cierra el panel para elegir en que boton se va a equipar el item
    public void CloseItemBtnOption() {
        itemBtnOptionMenu.SetActive(false);
    }
    // Función que permite asignar items a los botones
    public void UseItem(int btnNum) {
        activeItem.Use(btnNum);
        CloseItemBtnOption();
    }

}
