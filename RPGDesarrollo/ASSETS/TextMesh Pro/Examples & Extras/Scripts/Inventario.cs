using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    private bool muestraInventario; 
    public GameObject goInventario; 

    void Start()
    {
        muestraInventario = false;
        goInventario.SetActive(false); 
    }

    void Update()
    {
       
    }

    public void StatusInventario()
    {
        if (muestraInventario)
        {
            muestraInventario = false;
            goInventario.SetActive(false);
           //Cuando agreguen enemigos y demas borra los comentarios de la siguiente linea
           // time.timeScale = 1; //Pausa el tiempo al abrir el inventario
        }
        else
        {
            muestraInventario = true;
            goInventario.SetActive(true);
        }
    }
}
