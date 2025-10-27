using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VidasPlayer : MonoBehaviour
{
    public Image vidaPlayer;
    private float anchoVidaPlayer;
    public static int vida;
    private const int vidasINI = 5;
    private bool haMuerto;
    public static int perderVida = 1;
    public GameObject gameOver;

    void Start()
    {
        if (vidaPlayer == null)
        {
            return;
        }

        if (gameOver == null)
        {
            return;
        }

        anchoVidaPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasINI;
        gameOver.SetActive(false);
        DibujaVida(vida);
    }

    public void TomarDaño(int daño)
    {
        if (vida > 0 && perderVida == 1)
        {
            perderVida = 0;
            vida -= daño;
            if (vida < 0) vida = 0;

            DibujaVida(vida);
        }

        if (vida <= 0 && !haMuerto)
        {
            haMuerto = true;
            StartCoroutine(EjecutaMuerte());
        }
    }

    public void Curar(int cantidad)
    {
        if (haMuerto)
        {
            return;
        }

        int vidaAntes = vida;
        vida += cantidad;
        if (vida > vidasINI) vida = vidasINI;

        DibujaVida(vida);
    }

    public void DibujaVida(int vida)
    {
        RectTransform transformImagen = vidaPlayer.GetComponent<RectTransform>();
        float nuevaAnchura = anchoVidaPlayer * (float)vida / (float)vidasINI;
        transformImagen.sizeDelta = new Vector2(nuevaAnchura, transformImagen.sizeDelta.y);
    }

    IEnumerator EjecutaMuerte()
    {
        yield return new WaitForSeconds(1.2f);
        gameOver.SetActive(true);
    }

    public void RestaurarVidaCompleta()
    {
        vida = vidasINI;
        haMuerto = false;
        DibujaVida(vida);
    }
}