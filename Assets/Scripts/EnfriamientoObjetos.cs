using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnfriamientoObjetos : MonoBehaviour
{
    private float cdArco = 3f, cdWakizashi = 2f, cdShuriken = 1f;
    private float sigUsoArco = 0, sigUsoWaki = 0, sigUsoShu = 0;
    private bool cdArcoCompleto, cdWakiCompleto, cdShuComp;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > sigUsoArco) {
            cdArcoCompleto = true;
        }
        if (Time.time > sigUsoWaki) {
            cdWakiCompleto = true;
        }
        if (Time.time > sigUsoShu) {
            cdShuComp = true;
        }
    }

    public void ActivarCdrArco() {
        sigUsoArco = Time.time + cdArco;
        cdArcoCompleto = false;
    }
    public bool EstadoCdrArco() {
        return cdArcoCompleto;
    }

    public void ActivarCdrWaki() {
        sigUsoWaki = Time.time + cdWakizashi;
        cdWakiCompleto = false;
    }
    public bool EstadoCdrWaki() {
        return cdWakiCompleto;
    }
    public void ActivarCdrShuriken() {
        sigUsoShu = Time.time + cdShuriken;
        cdShuComp = false;
    }
    public bool EstadoCdrShuriken() {
        return cdShuComp;
    }

}
