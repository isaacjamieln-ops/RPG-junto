using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VidasPlayer : MonoBehaviour
{
    [Header("🩸 Configuración de Vida")]
    public Image vidaPlayer;              // Barra de vida del jugador
    private float anchoVidaPlayer;        // Tamaño original de la barra
    public static int vida;               // Vida actual del jugador
    private const int vidasINI = 5;       // Vida máxima

    [Header("⚔️ Daño y Muerte")]
    private bool haMuerto;
    public static int perderVida = 1;     // Control para evitar perder vida repetidamente
    public GameObject gameOver;           // Panel de Game Over

    void Start()
    {
        if (vidaPlayer == null)
        {
            Debug.LogError("⚠️ No se ha asignado 'vidaPlayer' en el Inspector.");
            return;
        }

        if (gameOver == null)
        {
            Debug.LogError("⚠️ No se ha asignado 'gameOver' en el Inspector.");
            return;
        }

        anchoVidaPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasINI;
        gameOver.SetActive(false);
        DibujaVida(vida);

        Debug.Log($"💚 [VidasPlayer] Vida inicial establecida en {vidasINI}");
    }

    // =========================================================
    // 🔻 Método para recibir daño
    // =========================================================
    public void TomarDaño(int daño)
    {
        if (vida > 0 && perderVida == 1)
        {
            perderVida = 0; // Evita daño múltiple inmediato
            vida -= daño;
            if (vida < 0) vida = 0;

            DibujaVida(vida);
            Debug.Log($"💢 Jugador recibió daño: -{daño} | Vida actual: {vida}");
        }

        if (vida <= 0 && !haMuerto)
        {
            haMuerto = true;
            StartCoroutine(EjecutaMuerte());
        }
    }

    // =========================================================
    // 💖 Método para curar (desde ítems del inventario)
    // =========================================================
    public void Curar(int cantidad)
    {
        if (haMuerto)
        {
            Debug.LogWarning("⚰️ No se puede curar: el jugador ya ha muerto.");
            return;
        }

        int vidaAntes = vida;
        vida += cantidad;
        if (vida > vidasINI) vida = vidasINI; // No superar la vida máxima

        DibujaVida(vida);

        Debug.Log($"💚 Jugador curado: +{cantidad} | Vida antes: {vidaAntes} → Vida actual: {vida}");
    }

    // =========================================================
    // 💡 Dibuja la barra de vida en la interfaz
    // =========================================================
    public void DibujaVida(int vida)
    {
        RectTransform transformImagen = vidaPlayer.GetComponent<RectTransform>();
        float nuevaAnchura = anchoVidaPlayer * (float)vida / (float)vidasINI;
        transformImagen.sizeDelta = new Vector2(nuevaAnchura, transformImagen.sizeDelta.y);
    }

    // =========================================================
    // ☠️ Corrutina de Game Over
    // =========================================================
    IEnumerator EjecutaMuerte()
    {
        Debug.Log("☠️ Jugador ha muerto. Mostrando pantalla de Game Over...");
        yield return new WaitForSeconds(1.2f);
        gameOver.SetActive(true);
    }

    // =========================================================
    // 🔄 Método opcional: resetear vida al máximo
    // =========================================================
    public void RestaurarVidaCompleta()
    {
        vida = vidasINI;
        haMuerto = false;
        DibujaVida(vida);
        Debug.Log("🩺 Vida restaurada completamente.");
    }
}