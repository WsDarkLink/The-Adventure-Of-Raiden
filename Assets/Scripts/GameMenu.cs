using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public string mainMenuGame;

    public Slider[] volumenSliders;
    public Toggle[] resolucionToggles;
    public int[] screenValores;
    int resolucionActiva;


    private bool menuExist;
    // Start is called before the first frame update
    void Start()
    {
        if (!menuExist)
        {
            menuExist = true;
            DontDestroyOnLoad(transform.gameObject);
            instance = this;           
        }
        else
        {
            Destroy(gameObject);
        }
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
            AudioManager.instance.PlaySFX(5);
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
        //GameManager.instance.SortItems();
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

    public void QuitGame() {
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerControlador.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(gameObject);
    }

    public void PlayBtnSound() {
        AudioManager.instance.PlaySFX(4);
    }

    public void PlayEquipItemSFX() {
        AudioManager.instance.PlaySFX(6);
    }
    //Para resoluciones con "escala" 16/9
    public void SetResolucionPantalla(int i) {
        if (resolucionToggles[i].isOn) {
            resolucionActiva = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenValores[i], (int)(screenValores[i] / aspectRatio), false);
        }
    }
    public void ResolucionCulera(int i) {
        if (resolucionToggles[0].isOn) {
            resolucionActiva = 0;
            Screen.SetResolution(screenValores[0], 768, false);
        }
    }

    public void SetFullScreen(bool isFullScreen) {
        for (int i = 0; i < resolucionToggles.Length; i++) {
            resolucionToggles[i].interactable = !isFullScreen;
        }
        if (isFullScreen) {
            //obtener la lista de resoluciones disponibles para el monitor
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            if (resolucionActiva > 0)
            {
                SetResolucionPantalla(resolucionActiva);
            }
            else {
                ResolucionCulera(resolucionActiva);
            }
        }        
    }

    public void SetVolumenMaestro(float valor) {
        for (int i = 0; i < AudioManager.instance.bgm.Length; i++)
        {
            AudioManager.instance.bgm[i].volume = valor;
        }
        for (int i = 0; i < AudioManager.instance.sfx.Length; i++)
        {
            AudioManager.instance.sfx[i].volume = valor;
        }
    }

    public void SetVolumenMusica(float valor)
    {
        for (int i = 0; i < AudioManager.instance.bgm.Length; i++) {
            AudioManager.instance.bgm[i].volume = valor;
        }
    }

    public void SetVolumenFX(float valor)
    {
        for (int i = 0; i < AudioManager.instance.sfx.Length; i++)
        {
            AudioManager.instance.sfx[i].volume = valor;
        }
    }

}
