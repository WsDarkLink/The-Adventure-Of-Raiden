 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool GmExist;
    public CharStats playerStats;
    public ArqueroStats arqueroStats;
    public MeleStats meleStats;
    public MagoStats magoStats;
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
        if (Input.GetKeyDown(KeyCode.O)) {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
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
    //funcion utilizada para reorganizar los items
    /*public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace == true)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemStock.Length - 1; i++)
            {
                if (itemStock[i] == "")
                {
                    itemStock[i] = itemStock[i + 1];
                    itemStock[i + 1] = "";

                    numDeItems[i] = numDeItems[i + 1];
                    numDeItems[i + 1] = 0;
                    if (itemStock[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }*/
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

    public void SaveData() {
        PlayerPrefs.SetString("Escena_Actual", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerControlador.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerControlador.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerControlador.instance.transform.position.z);

        //información del personaje
        PlayerPrefs.SetInt("Player_Level", playerStats.playerLevel);
        PlayerPrefs.SetInt("Daño_Espada", playerStats.dañoEspada);
        PlayerPrefs.SetFloat("Segundos_Waki", playerStats.segundosWakizashi);
        PlayerPrefs.SetInt("Daño_Arco", playerStats.dañoArco);
        PlayerPrefs.SetInt("Daño_Bomba", playerStats.dañoBomba);
        PlayerPrefs.SetInt("Daño_Shuriken", playerStats.dañoShuriken);
        PlayerPrefs.SetString("Item_K", playerStats.itemK);
        PlayerPrefs.SetString("Item_L", playerStats.itemL);
        //vida actual vida maxima corazones inicio
        PlayerPrefs.SetInt("Vida_Actual", PlayerControlador.instance.GetComponent<PlayerHealtManager>().vidaActualJugador);
        PlayerPrefs.SetInt("Vida_Maxima", PlayerControlador.instance.GetComponent<PlayerHealtManager>().vidaMaximaJugador);
        PlayerPrefs.SetInt("Corazones_Inicio", PlayerControlador.instance.GetComponent<PlayerHealtManager>().corazonesInicio);
        //inventario del personaje
        for (int i = 0; i < itemStock.Length; i++)
        {
            PlayerPrefs.SetString("Item_en_Inventario" + i, itemStock[i]);
            PlayerPrefs.SetInt("Item_Cantidad" + i, numDeItems[i]);
        }

        //información arquero 
        PlayerPrefs.SetInt("Vida_Arquero", arqueroStats.vidaArquero);
        PlayerPrefs.SetInt("Damage_Arquero", arqueroStats.dañoArquero);
        //Información Mago
        PlayerPrefs.SetInt("Vida_Mago", magoStats.vidaMago);
        PlayerPrefs.SetInt("Damage_Orbe_Fuego", magoStats.dañoOrbeFuego);
        PlayerPrefs.SetInt("DamageTotal_Orbe_Veneno", magoStats.dañoTotalVeneno);
        PlayerPrefs.SetInt("Damage_Orbe_Electrico", magoStats.dañoOrbeElectrico);
        PlayerPrefs.SetFloat("Tiempo_Paralisis", magoStats.tiempoParalisis);
        //Información mele 
        PlayerPrefs.SetInt("Vida_Mele", meleStats.vidaMele);
        PlayerPrefs.SetInt("Damage_Mele", meleStats.dañoMele);

    }

    public void LoadData() {
        PlayerControlador.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
        //Player Stats
        playerStats.playerLevel = PlayerPrefs.GetInt("Player_Level");
        playerStats.dañoEspada = PlayerPrefs.GetInt("Daño_Espada");
        playerStats.segundosWakizashi = PlayerPrefs.GetFloat("Segundos_Waki");
        playerStats.dañoArco = PlayerPrefs.GetInt("Daño_Arco");
        playerStats.dañoBomba = PlayerPrefs.GetInt("Daño_Bomba");
        playerStats.dañoShuriken = PlayerPrefs.GetInt("Daño_Shuriken");
        playerStats.itemK = PlayerPrefs.GetString("Item_K");
        playerStats.itemL = PlayerPrefs.GetString("Item_L");
        //vida actual vida maxima corazones inicio
        PlayerControlador.instance.GetComponent<PlayerHealtManager>().vidaActualJugador = PlayerPrefs.GetInt("Vida_Actual");
        PlayerControlador.instance.GetComponent<PlayerHealtManager>().vidaMaximaJugador = PlayerPrefs.GetInt("Vida_Maxima");
        PlayerControlador.instance.GetComponent<PlayerHealtManager>().corazonesInicio = PlayerPrefs.GetInt("Corazones_Inicio");
        UIManager.instance.UpdateVida();
        //inventario
        for (int i = 0; i < itemStock.Length; i++) {
            itemStock[i] = PlayerPrefs.GetString("Item_en_Inventario" + i);
            numDeItems[i] = PlayerPrefs.GetInt("Item_Cantidad" + i);
        }
        //información arquero 
        arqueroStats.vidaArquero = PlayerPrefs.GetInt("Vida_Arquero");
        arqueroStats.dañoArquero = PlayerPrefs.GetInt("Damage_Arquero");
        //Información Mago
        magoStats.vidaMago = PlayerPrefs.GetInt("Vida_Mago");
        magoStats.dañoOrbeFuego = PlayerPrefs.GetInt("Damage_Orbe_Fuego");
        magoStats.dañoTotalVeneno = PlayerPrefs.GetInt("DamageTotal_Orbe_Veneno");
        magoStats.dañoOrbeElectrico = PlayerPrefs.GetInt("Damage_Orbe_Electrico");
        magoStats.tiempoParalisis = PlayerPrefs.GetFloat("Tiempo_Paralisis");
        //Información mele 
        meleStats.vidaMele = PlayerPrefs.GetInt("Vida_Mele");
        meleStats.dañoMele = PlayerPrefs.GetInt("Damage_Mele");

    }

}
