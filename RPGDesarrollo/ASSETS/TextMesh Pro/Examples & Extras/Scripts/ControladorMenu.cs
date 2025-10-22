using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControladorMenu : MonoBehaviour
{
    public GameObject panelOpciones;
    public TextMeshProUGUI txtN1, txtN2, txtN3;

    public static int nivel = 1; // 1.Gato Soñador | 2.Gato Aventurero | 3.Gato Callejero

    void Start()
    {
        panelOpciones.SetActive(false);
        Time.timeScale = 1;
        DibujaNivel(nivel);
    }

    // --------- BOTÓN JUGAR ---------
    public void Jugar()
    {
        if (nivel == 1)
        {
            SceneManager.LoadScene("Bosque");
        }
        else if (nivel == 2)
        {
            SceneManager.LoadScene("Nivel2");
        }
        else if (nivel == 3)
        {
            SceneManager.LoadScene("Nivel3");
        }
    }

    // --------- MÉTODOS DE SELECCIÓN DE NIVEL ---------
    public void Nivel1()
    {
        nivel = 1;
        DibujaNivel(nivel);
    }

    public void Nivel2()
    {
        nivel = 2;
        DibujaNivel(nivel);
    }

    public void Nivel3()
    {
        nivel = 3;
        DibujaNivel(nivel);
    }

    // --------- CAMBIO DE COLOR SEGÚN SELECCIÓN ---------
    private void DibujaNivel(int n)
    {
        // Texto neutral → blanco
        Color colorBase = Color.white;
        txtN1.color = colorBase;
        txtN2.color = colorBase;
        txtN3.color = colorBase;

        // Colores por nivel (visibles sobre el botón)
        Color verde = new Color(0.4f, 1f, 0.4f);      // Nivel 1: Verde (#66FF66)
        Color amarillo = new Color(1f, 0.95f, 0.4f);  // Nivel 2: Amarillo (#FFEE66)
        Color rojo = new Color(1f, 0.5f, 0.5f);       // Nivel 3: Rojo (#FF8080)

        switch (n)
        {
            case 1:
                txtN1.color = verde;
                break;
            case 2:
                txtN2.color = amarillo;
                break;
            case 3:
                txtN3.color = rojo;
                break;
        }
    }

    // --------- MOSTRAR / OCULTAR OPCIONES ---------
    public void AbreOpciones()
    {
        panelOpciones.SetActive(true);
    }

    public void CierraOpciones()
    {
        panelOpciones.SetActive(false);
    }

    // --------- SALIR DEL JUEGO ---------
    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("El juego se ha cerrado."); // Solo visible en el editor
    }
}