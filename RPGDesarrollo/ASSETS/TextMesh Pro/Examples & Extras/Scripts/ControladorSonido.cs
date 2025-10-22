using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSonido : MonoBehaviour
{
    public Slider slVol;
    public AudioSource reproductor;
    public Button btnapagarSnd;
    public Sprite snd0, snd1;

    public static float volumen = 1f; // Volumen inicial (0-1)
    public static bool statusAudio = true; // true = encendido, false = apagado

    void Start()
    {
        // ----------- CONFIGURAR VOLUMEN INICIAL -----------
        if (volumen == 0)
        {
            Debug.Log("Sin modificar el volumen inicial");
        }
        else
        {
            Debug.Log("Vol: " + volumen.ToString());
            reproductor.volume = volumen;
            slVol.value = volumen;
        }

        // ----------- CONFIGURAR ESTADO DE AUDIO (ON/OFF) -----------
        if (statusAudio == false)
        {
            reproductor.Stop();
            btnapagarSnd.image.sprite = snd1; // Icono de apagado
        }
        else
        {
            reproductor.Play();
            btnapagarSnd.image.sprite = snd0; // Icono de encendido
        }
    }

    // ----------- SLIDER PARA CAMBIAR VOLUMEN -----------
    public void ModificaVolumen()
    {
        volumen = slVol.value;
        reproductor.volume = volumen;
    }

    // ----------- BOTÓN PARA ENCENDER / APAGAR AUDIO -----------
    public void ActivaAudio()
    {
        if (statusAudio)
        {
            reproductor.Stop();
            statusAudio = false;
            btnapagarSnd.image.sprite = snd1; // Icono apagado
        }
        else
        {
            reproductor.Play();
            statusAudio = true;
            btnapagarSnd.image.sprite = snd0; // Icono encendido
        }
    }
}
