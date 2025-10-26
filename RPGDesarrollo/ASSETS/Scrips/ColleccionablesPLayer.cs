using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColeccionablesPlayer : MonoBehaviour
{
    private GameObject player;
    public static string objAColeccionar = "";
    private Inventario1 inventario1;

    void Start()
    {
        player = GameObject.Find("Player");
        objAColeccionar = "";
        inventario1 = FindObjectOfType<Inventario1>();
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        string tag = obj.tag;

        if (tag == "PCV")
        {
            VidasPlayer.vida++;
            player.GetComponent<VidasPlayer>().DibujaVida(VidasPlayer.vida);
            Destroy(obj.gameObject);
        }
        else if (tag == "PCM" || tag == "MPV" || tag == "MPM" || tag == "MO" ||
                 tag == "EX" || tag == "GE" || tag == "PEZ")
        {
            AplicaCambios(obj);
        }
        else if (tag == "EM" || tag == "BS" || tag == "FM" || tag == "BA" ||
                 tag == "AVI" || tag == "AM" || tag == "AV" || tag == "AAL")
        {
            // Solo una vez por partida
            if (!inventario1.YaTieneObjeto(tag))
                AplicaCambios(obj);
            else
                Destroy(obj.gameObject);
        }
    }

    private void AplicaCambios(Collider2D obj)
    {
        objAColeccionar = obj.tag;
        inventario1.EscribeEnArreglo(obj.tag);
        Destroy(obj.gameObject);
    }
}
