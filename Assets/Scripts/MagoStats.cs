using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoStats : MonoBehaviour
{
    public int dañoOrbeFuego = -2;
    public int dañoOrbeElectrico = -1;
    public float tiempoParalisis = 1.5f;
    public int dañoTotalVeneno = 4;
    public int vidaMago = 50;

    public void LevelUp() {
        vidaMago = 100;
        dañoOrbeFuego = -4;
        dañoOrbeElectrico = -2;
        tiempoParalisis = 2f;
        dañoTotalVeneno = 8;
    }


}
