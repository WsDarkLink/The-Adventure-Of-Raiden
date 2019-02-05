using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealtManager : MonoBehaviour
{

    public int vidaMaximaEnemigo;
    public int vidaActualEnemigo;
    public string tipoEnemigo;
    public int probabilidadDrop;
    public GameObject bombaInstancia;
    public GameManager Manager;
    // Start is called before the first frame update
    void Start()
    {
        Manager = FindObjectOfType<GameManager>();
        if (tipoEnemigo == "Mago") {
            vidaMaximaEnemigo = Manager.magoStats.vidaMago;
        }
        if (tipoEnemigo == "Arquero")
        {
            vidaMaximaEnemigo = Manager.arqueroStats.vidaArquero;
        }
        if (tipoEnemigo == "Mele")
        {
            vidaMaximaEnemigo = Manager.meleStats.vidaMele;
        }

        vidaActualEnemigo = vidaMaximaEnemigo;
        probabilidadDrop = Random.Range(0,10);
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaActualEnemigo <= 0)
        {
            if (probabilidadDrop < 2) {
                Instantiate(bombaInstancia, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    public void HurtEnemy(int damage)
    {
        vidaActualEnemigo -= damage;
    }

    public void SetVidaMaxima()
    {
        vidaActualEnemigo = vidaMaximaEnemigo;
    }
}
