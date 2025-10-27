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
        // NUEVO SISTEMA - ItemRecolectable (prioridad)
        ItemRecolectable item = obj.GetComponent<ItemRecolectable>();

        if (item != null)
        {
            objAColeccionar = item.idItem;
            inventario1.EscribeEnArreglo(item.idItem);
            Destroy(obj.gameObject);
        }
        else
        {
            // SISTEMA ANTIGUO - Tags (compatibilidad hacia atrás)
            string tag = obj.tag;

            if (tag == "PCV")
            {
                VidasPlayer.vida++;
                player.GetComponent<VidasPlayer>().DibujaVida(VidasPlayer.vida);
                Destroy(obj.gameObject);
            }
            else if (tag == "PCM")
            {
                //Aumentar Mana
                AplicaCambios(obj);
            }
            else if (tag == "MPV")
            {
                //Aumentar Maxima Vitalidad
                AplicaCambios(obj);
            }
            else if (tag == "MPM")
            {
                //Aumentar Mana Maximo
                AplicaCambios(obj);
            }
            else if (tag == "EM")
            {
                //Obtener Espada Maestra
                AplicaCambios(obj);
            }
            else if (tag == "BS")
            {
                //Obtener Baculo Sagrado
                AplicaCambios(obj);
            }
            else if (tag == "FM")
            {
                //Obtener FiloMiau
                AplicaCambios(obj);
            }
            else if (tag == "BA")
            {
                //Obtener Baculo del anillo
                AplicaCambios(obj);
            }
            else if (tag == "AVI")
            {
                //Obtener anilllo Vitalidad
                AplicaCambios(obj);
            }
            else if (tag == "AM")
            {
                //Obtener anilllo Mana
                AplicaCambios(obj);
            }
            else if (tag == "AV")
            {
                //Obtener anilllo Velocidad
                AplicaCambios(obj);
            }
            else if (tag == "AAL")
            {
                //Obtener anilllo Almas
                AplicaCambios(obj);
            }
            else if (tag == "MO")
            {
                //Aumentar Monedas
                AplicaCambios(obj);
            }
            else if (tag == "EX")
            {
                //Aumentar Experiencia
                AplicaCambios(obj);
            }
            else if (tag == "GE")
            {
                //Aumentar Gemas
                AplicaCambios(obj);
            }
            else if (tag == "PEZ")
            {
                //Aumentar Pescados
                AplicaCambios(obj);
            }
        }
    }

    private void AplicaCambios(Collider2D obj)
    {
        objAColeccionar = obj.tag;
        inventario1.EscribeEnArreglo(obj.tag);
        Destroy(obj.gameObject);
    }
}