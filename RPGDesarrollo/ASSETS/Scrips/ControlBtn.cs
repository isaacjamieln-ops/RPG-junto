using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class ControlBtn : MonoBehaviour
{
  public void volverMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}

