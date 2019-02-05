using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArqueroStats : MonoBehaviour
{
    public int vidaArquero = 50;
    public int dañoArquero = -1;

    public void LevelUp() {
        vidaArquero = 100;
        dañoArquero = -2;
    }
}
