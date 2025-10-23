using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text puntos;
    public GameObject[] vidas;

    void Start()
    {
        // Inicializar puntos si está nulo
        if (puntos == null)
        {
            puntos = GetComponentInChildren<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (puntos != null && GameManager.Instance != null)
        {
            puntos.text = GameManager.Instance.PuntosTotales.ToString();
        }
    }

    public void ActualizarPuntos(int PuntosTotales)
    {
        if (puntos != null)
        {
            puntos.text = PuntosTotales.ToString();
        }
    }
    
    public void DesactivarVida(int indice)
    {
        if (vidas != null && indice >= 0 && indice < vidas.Length && vidas[indice] != null)
        {
            vidas[indice].SetActive(false);
        }
    }
    
    public void ActivarVida(int indice) 
    {
        if (vidas != null && indice >= 0 && indice < vidas.Length && vidas[indice] != null)
        {
            vidas[indice].SetActive(true);
        }
    }
}