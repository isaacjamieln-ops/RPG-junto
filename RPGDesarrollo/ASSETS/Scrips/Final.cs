using UnityEngine;

public class Final : MonoBehaviour
{
    public string tagJugador = "Player";       // Tag del jugador
    public GameObject pantallaFinal;           // Asigna el panel final desde el inspector

    private bool juegoFinalizado = false;

    private void Start()
    {
        // Asegúrate de que la pantalla final esté desactivada al inicio
        if (pantallaFinal != null)
            pantallaFinal.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador tocó la llave
        if (other.CompareTag(tagJugador) && !juegoFinalizado)
        {
            juegoFinalizado = true;

            // Activa la pantalla final
            if (pantallaFinal != null)
                pantallaFinal.SetActive(true);

            // Desactiva la llave para simular que fue tomada
            gameObject.SetActive(false);

            // Opcional: Detén el tiempo del juego (pausa total)
            Time.timeScale = 0f;
        }
    }
}