using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public int playerLevel = 1;
    public int dañoEspada = 25;
    public float segundosWakizashi = 2;
    public int dañoArco = 10;
    public int dañoBomba = 40;
    public int dañoShuriken = 5;

    public string itemK, itemL;
    
    // aumenta el daño de los objetos cuando se sube de nivel
    public void AddLevel() {
        playerLevel++;

        dañoEspada += 10;
        dañoShuriken += 10;
    }

}
