using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyPickup : MonoBehaviour
{
    [Header("Configuración")]
    public string tagJugador = "Player";     // Tag del jugador
    public bool finalizarJuego = true;       // Si el juego termina al recoger la llave
    public GameObject PantallaFinal;         // Pantalla de victoria (UI)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detectar si el jugador toca la llave
        if (other.CompareTag(tagJugador))
        {
            Debug.Log("¡Llave recogida!");

            // Desactivar la llave (ya fue recogida)
            gameObject.SetActive(false);

            // Ejecutar la secuencia final
            if (finalizarJuego)
            {
                StartCoroutine(EjecutaFinal());
            }
        }
    }

    private IEnumerator EjecutaFinal()
    {
        Debug.Log("¡Has ganado el juego!");

        // Esperar 1.2 segundos antes de mostrar la pantalla
        yield return new WaitForSeconds(1.2f);

        if (PantallaFinal != null)
        {
            PantallaFinal.SetActive(true);
        }

        // Termina el juego (solo en compilado)
        Application.Quit();

        // Alternativamente, podrías cargar una escena de victoria:
        // SceneManager.LoadScene("EscenaVictoria");
    }
}
