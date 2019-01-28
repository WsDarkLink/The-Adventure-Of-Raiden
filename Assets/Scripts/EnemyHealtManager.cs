using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealtManager : MonoBehaviour
{

    public int vidaMaximaEnemigo;
    public int vidaActualEnemigo;
    // Start is called before the first frame update
    void Start()
    {
        vidaActualEnemigo = vidaMaximaEnemigo;
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaActualEnemigo <= 0)
        {
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
