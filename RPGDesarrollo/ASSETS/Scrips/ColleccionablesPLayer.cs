using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColeccionablesPlayer : MonoBehaviour
{
    [Header("Referencias")]
    public Inventario1 inventario;
    
    [Header("Configuración")]
    public static string objAColeccionar = "";

    void Start()
    {
        // Buscar automáticamente el inventario si no está asignado
        if (inventario == null)
        {
            inventario = FindObjectOfType<Inventario1>();
            if (inventario == null)
            {
                Debug.LogError(" No se encontró el componente Inventario1 en la escena");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string objectTag = collision.tag;
        Debug.Log($" Trigger detectado con: {collision.name} - Tag: {objectTag}");
        
        // Sistema de tags para todos los objetos coleccionables
        if (objectTag == "PCV")
        {
            ManejarPCV(collision);
        }
        else if (objectTag == "PCM")
        {
            ManejarPCM(collision);
        }
        else if (objectTag == "MPV")
        {
            ManejarMPV(collision);
        }
        else if (objectTag == "MPM")
        {
            ManejarMPM(collision);
        }
        else if (objectTag == "MO")
        {
            ManejarMO(collision);
        }
        else if (objectTag == "EX")
        {
            ManejarEX(collision);
        }
        else if (objectTag == "GE")
        {
            ManejarGE(collision);
        }
        else if (objectTag == "PEZ")
        {
            ManejarPEZ(collision);
        }
        else if (objectTag == "EM")
        {
            ManejarObjetoUnico(collision, objectTag);
        }
        else if (objectTag == "BS")
        {
            ManejarObjetoUnico(collision, objectTag);
        }
        else if (objectTag == "FM")
        {
            ManejarObjetoUnico(collision, objectTag);
        }
        else if (objectTag == "BA")
        {
            ManejarObjetoUnico(collision, objectTag);
        }
        else if (objectTag == "AVI")
        {
            ManejarObjetoUnico(collision, objectTag);
        }
        else if (objectTag == "AM")
        {
            ManejarObjetoUnico(collision, objectTag);
        }
        else if (objectTag == "AV")
        {
            ManejarObjetoUnico(collision, objectTag);
        }
        else if (objectTag == "AAL")
        {
            ManejarObjetoUnico(collision, objectTag);
        }
        else
        {
            Debug.Log($" Objeto con tag no reconocido: {objectTag}");
        }
    }

    private void ManejarPCV(Collider2D obj)
    {
        if (inventario != null)
        {
            inventario.EscribeEnArreglo("PCV");
            Debug.Log(" PCV agregada al inventario");
        }
        else
        {
            Debug.LogError(" Inventario no encontrado para PCV");
        }
        
        // Aumentar vida inmediatamente
        VidasPlayer vidas = GetComponent<VidasPlayer>();
        if (vidas != null)
        {
            VidasPlayer.vida++;
            vidas.DibujaVida(VidasPlayer.vida);
            Debug.Log(" Vida aumentada inmediatamente");
        }
        
        Destroy(obj.gameObject);
        objAColeccionar = "PCV";
        Debug.Log(" PCV recolectada y destruida");
    }

    private void ManejarPCM(Collider2D obj)
    {
        if (inventario != null)
        {
            inventario.EscribeEnArreglo("PCM");
            Debug.Log(" PCM agregada al inventario");
        }
        Destroy(obj.gameObject);
        objAColeccionar = "PCM";
        Debug.Log(" PCM recolectada");
    }

    private void ManejarMPV(Collider2D obj)
    {
        if (inventario != null)
        {
            inventario.EscribeEnArreglo("MPV");
            Debug.Log(" MPV agregada al inventario");
        }
        Destroy(obj.gameObject);
        objAColeccionar = "MPV";
        Debug.Log(" MPV recolectada");
    }

    private void ManejarMPM(Collider2D obj)
    {
        if (inventario != null)
        {
            inventario.EscribeEnArreglo("MPM");
            Debug.Log(" MPM agregada al inventario");
        }
        Destroy(obj.gameObject);
        objAColeccionar = "MPM";
        Debug.Log(" MPM recolectada");
    }

    private void ManejarMO(Collider2D obj)
    {
        if (inventario != null)
        {
            inventario.EscribeEnArreglo("MO");
            Debug.Log(" MO agregada al inventario");
        }
        Destroy(obj.gameObject);
        objAColeccionar = "MO";
        Debug.Log(" Monedas recolectadas");
    }

    private void ManejarEX(Collider2D obj)
    {
        if (inventario != null)
        {
            inventario.EscribeEnArreglo("EX");
            Debug.Log(" EX agregada al inventario");
        }
        Destroy(obj.gameObject);
        objAColeccionar = "EX";
        Debug.Log(" Experiencia recolectada");
    }

    private void ManejarGE(Collider2D obj)
    {
        if (inventario != null)
        {
            inventario.EscribeEnArreglo("GE");
            Debug.Log(" GE agregada al inventario");
        }
        Destroy(obj.gameObject);
        objAColeccionar = "GE";
        Debug.Log(" Gemas recolectadas");
    }

    private void ManejarPEZ(Collider2D obj)
    {
        if (inventario != null)
        {
            inventario.EscribeEnArreglo("PEZ");
            Debug.Log(" PEZ agregada al inventario");
        }
        Destroy(obj.gameObject);
        objAColeccionar = "PEZ";
        Debug.Log(" Pescado recolectado");
    }

    private void ManejarObjetoUnico(Collider2D obj, string tag)
    {
        Debug.Log($" Intentando recolectar objeto único: {tag}");
        
        if (inventario != null)
        {
            // Verificar si ya tiene el objeto único
            if (!inventario.YaTieneObjeto(tag))
            {
                inventario.EscribeEnArreglo(tag);
                Destroy(obj.gameObject);
                objAColeccionar = tag;
                Debug.Log($" {tag} recolectado (objeto único)");
            }
            else
            {
                Debug.Log($"ℹ Ya tienes el objeto único {tag}");
                Destroy(obj.gameObject);
            }
        }
        else
        {
            Debug.LogError($" Inventario no encontrado para {tag}");
            Destroy(obj.gameObject);
        }
    }

    // Método para abrir/cerrar inventario desde otros scripts
    public void ToggleInventario()
    {
        if (inventario != null)
        {
            inventario.ToggleInventario();
        }
    }
}