using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleStats : MonoBehaviour
{
    public int vidaMele = 75;
    public int dañoMele = -2;

    public void LevelUp() {
        vidaMele = 150;
        dañoMele = -4;
    }
}
