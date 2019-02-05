using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public GameObject continuarBtn;
    public string loadGameScene;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Escena_Actual"))
        {
            continuarBtn.SetActive(true);
        }
        else {
            continuarBtn.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame() {
        SceneManager.LoadScene(newGameScene);
    }

    public void Continuar() {
        SceneManager.LoadScene(loadGameScene);   
    }

    public void SalirDelJuego() {
        Application.Quit();
    }

}
