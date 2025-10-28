using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour
{
    // Nombre de la escena del menú principal (asegúrate que esté añadida en "Build Settings")
    [SerializeField] private string nombreMenu = "MenuPrincipal";

    // --------- MÉTODO PARA VOLVER AL MENÚ ---------
    public void VolverAlMenu()
    {
        // Restablece el tiempo en caso de estar pausado
        Time.timeScale = 1f;

        // Carga la escena del menú principal
        SceneManager.LoadScene(nombreMenu);
    }

    // --------- OPCIONAL: Atajo con tecla ESC ---------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VolverAlMenu();
        }
    }
}
