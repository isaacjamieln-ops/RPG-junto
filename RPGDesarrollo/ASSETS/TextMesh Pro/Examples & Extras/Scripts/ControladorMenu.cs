using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolverMenu : MonoBehaviour
{
    [SerializeField] private string nombreMenu = "MenuPrincipal";
    [SerializeField] private Button botonMenuRegreso;

    void Start()
    {
        if (botonMenuRegreso != null)
        {
            botonMenuRegreso.onClick.AddListener(VolverAlMenu);
        }
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VolverAlMenu();
        }
    }
}