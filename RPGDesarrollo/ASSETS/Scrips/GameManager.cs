using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public HUD hud;

    public int PuntosTotales { get; private set; }

    private int vidas = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! mas de un GameManager en Escena");
        }
    }
    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
        hud.ActualizarPuntos(PuntosTotales);
    }

    public void PerderVida()
    {
        vidas -= vidas - 1;
//SOLO SI QUEREMOS IMPLEMENTARLO
        //if (vidas == 0)
       // {
            //Reiniciar Nivel
            //SceneManager.LoadScene("Bosque");
        //}

        hud.DesactivarVida(vidas);
    }
    public bool RecuperarVida()
    {
        if (vidas ==3)
        {
            return false;
        }
        hud.ActivarVida(vidas);
        vidas += 1;
        return true; 
    }
}