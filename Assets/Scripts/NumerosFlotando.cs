using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumerosFlotando : MonoBehaviour
{
    public float velocidadMov;
    public int damageNum;
    public Text displayTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayTxt.text = "" + damageNum;
        transform.position = new Vector3(transform.position.x, transform.position.y + (velocidadMov * Time.deltaTime), transform.position.z);
    }
}
