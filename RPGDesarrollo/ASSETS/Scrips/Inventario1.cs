using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario1 : MonoBehaviour
{
    [Header("Configuración del Inventario")]
    public GameObject panelInventario;
    public int tamañoInventario = 20;
    
    [Header("Sprites de Objetos")]
    public Sprite PCV, PCM, MPV, MPM, EM, BS, FM, BA, AVI, AM, AV, AAL, MO, EX, GE, PEZ, contenedor;

    // Arreglo interno del inventario
    private string[] slots;
    private int[] cantidades;
    
    // Referencias a los botones del UI
    private Button[] botonesInventario;
    private bool inventarioAbierto = false;

    void Start()
    {
        InicializarInventario();
        ObtenerReferenciasBotones();
        CerrarInventario();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventario();
        }
    }

    private void InicializarInventario()
    {
        slots = new string[tamañoInventario];
        cantidades = new int[tamañoInventario];
        
        for (int i = 0; i < tamañoInventario; i++)
        {
            slots[i] = "";
            cantidades[i] = 0;
        }
    }

    private void ObtenerReferenciasBotones()
    {
        if (panelInventario != null)
        {
            botonesInventario = panelInventario.GetComponentsInChildren<Button>();
            ConectarBotonesConFunciones();
        }
    }

    private void ConectarBotonesConFunciones()
    {
        if (botonesInventario == null) return;

        for (int i = 0; i < botonesInventario.Length; i++)
        {
            int index = i;
            botonesInventario[i].onClick.RemoveAllListeners();
            botonesInventario[i].onClick.AddListener(() => UsarItemDesdeSlot(index));
        }
    }

    public void EscribeEnArreglo(string idItem)
    {
        // Buscar si el item ya existe en el inventario
        int slotExistente = BuscarSlotPorItem(idItem);
        
        if (slotExistente != -1 && !EsObjetoUnico(idItem))
        {
            // Incrementar cantidad si ya existe y no es único
            cantidades[slotExistente]++;
            ActualizarUI();
        }
        else
        {
            // Buscar slot vacío para nuevo item
            for (int i = 0; i < tamañoInventario; i++)
            {
                if (string.IsNullOrEmpty(slots[i]))
                {
                    slots[i] = idItem;
                    cantidades[i] = 1;
                    ActualizarUI();
                    break;
                }
            }
        }
        
        Debug.Log($" Item agregado: {idItem}");
    }

    private int BuscarSlotPorItem(string idItem)
    {
        for (int i = 0; i < tamañoInventario; i++)
        {
            if (slots[i] == idItem)
                return i;
        }
        return -1;
    }

    public bool YaTieneObjeto(string idItem)
    {
        return BuscarSlotPorItem(idItem) != -1;
    }

    private bool EsObjetoUnico(string idItem)
    {
        string[] objetosUnicos = { "EM", "BS", "FM", "BA", "AVI", "AM", "AV", "AAL" };
        foreach (string obj in objetosUnicos)
        {
            if (idItem == obj) return true;
        }
        return false;
    }

    private void ActualizarUI()
    {
        if (botonesInventario == null) return;

        for (int i = 0; i < tamañoInventario && i < botonesInventario.Length; i++)
        {
            Image imagen = botonesInventario[i].GetComponent<Image>();
            Text texto = botonesInventario[i].GetComponentInChildren<Text>();

            if (string.IsNullOrEmpty(slots[i]))
            {
                imagen.sprite = contenedor;
                if (texto != null) texto.text = "";
            }
            else
            {
                imagen.sprite = ObtenerSpritePorID(slots[i]);
                if (texto != null)
                {
                    texto.text = EsObjetoUnico(slots[i]) ? "" : $"x{cantidades[i]}";
                }
            }
        }
    }

    private Sprite ObtenerSpritePorID(string idItem)
    {
        switch (idItem)
        {
            case "PCV": return PCV;
            case "PCM": return PCM;
            case "MPV": return MPV;
            case "MPM": return MPM;
            case "EM": return EM;
            case "BS": return BS;
            case "FM": return FM;
            case "BA": return BA;
            case "AVI": return AVI;
            case "AM": return AM;
            case "AV": return AV;
            case "AAL": return AAL;
            case "MO": return MO;
            case "EX": return EX;
            case "GE": return GE;
            case "PEZ": return PEZ;
            default: return contenedor;
        }
    }

    private void UsarItemDesdeSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= tamañoInventario) return;
        if (string.IsNullOrEmpty(slots[slotIndex])) return;

        string itemID = slots[slotIndex];
        Debug.Log($" Intentando usar item: {itemID} del slot {slotIndex}");

        // Lógica de uso de items
        switch (itemID)
        {
            case "PCV":
                UsarPocionVida(slotIndex);
                break;
            case "PCM":
                UsarPocionMana(slotIndex);
                break;
            default:
                Debug.Log($"ℹEl objeto {itemID} no tiene función de uso definida");
                break;
        }
    }

    private void UsarPocionVida(int slotIndex)
    {
        VidasPlayer vidasPlayer = FindObjectOfType<VidasPlayer>();
        if (vidasPlayer != null)
        {
            vidasPlayer.Curar(1);
            cantidades[slotIndex]--;
            
            if (cantidades[slotIndex] <= 0)
            {
                slots[slotIndex] = "";
                cantidades[slotIndex] = 0;
            }
            
            ActualizarUI();
            Debug.Log(" Poción de vida usada");
        }
        else
        {
            Debug.LogError(" No se encontró VidasPlayer en la escena");
        }
    }

    private void UsarPocionMana(int slotIndex)
    {
        // Aquí puedes agregar la lógica para el mana
        cantidades[slotIndex]--;
        
        if (cantidades[slotIndex] <= 0)
        {
            slots[slotIndex] = "";
            cantidades[slotIndex] = 0;
        }
        
        ActualizarUI();
        Debug.Log("🔵 Poción de mana usada");
    }

    public void ToggleInventario()
    {
        inventarioAbierto = !inventarioAbierto;
        
        if (panelInventario != null)
        {
            panelInventario.SetActive(inventarioAbierto);
            if (inventarioAbierto)
            {
                ActualizarUI();
                Time.timeScale = 0; // Pausa el juego
            }
            else
            {
                Time.timeScale = 1; // Reanuda el juego
            }
        }
    }

    public void AbrirInventario()
    {
        if (panelInventario != null)
        {
            panelInventario.SetActive(true);
            inventarioAbierto = true;
            ActualizarUI();
            Time.timeScale = 0;
        }
    }

    public void CerrarInventario()
    {
        if (panelInventario != null)
        {
            panelInventario.SetActive(false);
            inventarioAbierto = false;
            Time.timeScale = 1;
        }
    }
}