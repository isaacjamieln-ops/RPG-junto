using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario1 : MonoBehaviour
{
    private bool muestraInventario1;
    public GameObject goInventario1;

    [SerializeField] private string[] valoresInventario1 = new string[20]; // Arreglo de objetos en el inventario

    // Contadores de objetos acumulables
    private int numPCV, numPCM, numMPV, numMPM, numMO, numEX, numGE, numPEZ;

    // Referencias a botones
    private Button[] botonesInventario1;

    // Sprites de objetos
    public Sprite PCV, PCM, MPV, MPM, EM, BS, FM, BA, AVI, AM, AV, AAL, MO, EX, GE, PEZ, contenedor;

    // Referencia al sistema de vida del jugador
    private VidasPlayer vidasPlayer;

    void Start()
    {
        if (goInventario1 == null)
            Debug.LogError("⚠️ [Inventario1] goInventario1 no está asignado en el inspector.");

        muestraInventario1 = false;
        BorraArreglo();

        // Inicializar contadores
        numPCV = numPCM = numMPV = numMPM = numMO = numEX = numGE = numPEZ = 0;

        // Obtener todos los botones del panel de inventario
        if (goInventario1 != null)
            botonesInventario1 = goInventario1.GetComponentsInChildren<Button>(true);
        else
            botonesInventario1 = new Button[0];

        // Buscar VidasPlayer automáticamente
        vidasPlayer = FindObjectOfType<VidasPlayer>();
        if (vidasPlayer == null)
            Debug.LogWarning("⚠️ [Inventario1] No se encontró 'VidasPlayer' en la escena.");

        // CONECTAR BOTONES AUTOMÁTICAMENTE
        ConectarBotonesConFunciones();
    }

    public void StatusInventario()
    {
        if (goInventario1 == null) return;

        if (muestraInventario1)
        {
            muestraInventario1 = false;
            goInventario1.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            muestraInventario1 = true;
            goInventario1.SetActive(true);
            ActualizaBotones();
            Time.timeScale = 0;
        }
    }

    // ✅ NUEVO: Conectar botones con funciones de uso
    private void ConectarBotonesConFunciones()
    {
        if (botonesInventario1 == null || botonesInventario1.Length == 0) return;

        for (int i = 0; i < botonesInventario1.Length; i++)
        {
            int index = i; // Importante: capturar el índice para el closure
            Button boton = botonesInventario1[i];
            
            // Remover listeners previos y agregar nuevo
            boton.onClick.RemoveAllListeners();
            boton.onClick.AddListener(() => OnBotonInventarioClickeado(index));
        }
        
        Debug.Log("🔗 [Inventario1] Botones conectados con funciones de uso");
    }

    // ✅ NUEVO: Método que se ejecuta cuando se clickea un botón del inventario
    private void OnBotonInventarioClickeado(int indiceBoton)
    {
        if (indiceBoton < 0 || indiceBoton >= valoresInventario1.Length) return;

        string objeto = valoresInventario1[indiceBoton];
        Debug.Log($"🖱️ [Inventario1] Botón clickeado: {objeto} en posición {indiceBoton}");

        if (!string.IsNullOrEmpty(objeto))
        {
            UsarItem(objeto);
        }
    }

    private void ActualizaBotones()
    {
        if (botonesInventario1 == null || botonesInventario1.Length == 0) return;

        for (int i = 0; i < valoresInventario1.Length && i < botonesInventario1.Length; i++)
        {
            string objeto = valoresInventario1[i];
            if (string.IsNullOrEmpty(objeto))
            {
                botonesInventario1[i].GetComponent<Image>().sprite = contenedor;
                Text textoBoton = botonesInventario1[i].GetComponentInChildren<Text>();
                if (textoBoton != null) textoBoton.text = "";
            }
            else
            {
                DibujaElementos(i);
            }
        }
    }

    public void EscribeEnArreglo(string objeto)
    {
        Debug.Log("📦 [Inventario1] Recolectado objeto: " + objeto);
        int pos = VerificaEnArreglo(objeto);

        if (pos == -1) // No está en el inventario
        {
            for (int i = 0; i < valoresInventario1.Length; i++)
            {
                if (valoresInventario1[i] == "")
                {
                    valoresInventario1[i] = objeto;
                    DibujaElementos(i);
                    break;
                }
            }
        }
        else // Ya está, actualiza solo si no es único
        {
            if (!EsObjetoUnico(objeto))
            {
                DibujaElementos(pos);
            }
        }
    }

    private int VerificaEnArreglo(string objeto)
    {
        for (int i = 0; i < valoresInventario1.Length; i++)
        {
            if (valoresInventario1[i] == objeto)
                return i;
        }
        return -1;
    }

    public bool YaTieneObjeto(string objeto)
    {
        return VerificaEnArreglo(objeto) != -1;
    }

    private bool EsObjetoUnico(string objeto)
    {
        return objeto == "EM" || objeto == "BS" || objeto == "FM" || objeto == "BA" ||
               objeto == "AVI" || objeto == "AM" || objeto == "AV" || objeto == "AAL";
    }

    private void DibujaElementos(int i)
    {
        if (i < 0 || i >= botonesInventario1.Length) return;

        string objeto = valoresInventario1[i];
        Button boton = botonesInventario1[i];
        Sprite icono = contenedor;
        string texto = "";

        switch (objeto)
        {
            case "PCV": icono = PCV; numPCV++; texto = "x" + numPCV; break;
            case "PCM": icono = PCM; numPCM++; texto = "x" + numPCM; break;
            case "MPV": icono = MPV; numMPV++; texto = "x" + numMPV; break;
            case "MPM": icono = MPM; numMPM++; texto = "x" + numMPM; break;
            case "MO": icono = MO; numMO++; texto = "x" + numMO; break;
            case "EX": icono = EX; numEX++; texto = "x" + numEX; break;
            case "GE": icono = GE; numGE++; texto = "x" + numGE; break;
            case "PEZ": icono = PEZ; numPEZ++; texto = "x" + numPEZ; break;

            // Objetos únicos
            case "EM": icono = EM; texto = ""; break;
            case "BS": icono = BS; texto = ""; break;
            case "FM": icono = FM; texto = ""; break;
            case "BA": icono = BA; texto = ""; break;
            case "AVI": icono = AVI; texto = ""; break;
            case "AM": icono = AM; texto = ""; break;
            case "AV": icono = AV; texto = ""; break;
            case "AAL": icono = AAL; texto = ""; break;
        }

        boton.GetComponent<Image>().sprite = icono;
        Text textoBoton = boton.GetComponentInChildren<Text>();
        if (textoBoton != null) textoBoton.text = texto;
    }

    private void BorraArreglo()
    {
        for (int i = 0; i < valoresInventario1.Length; i++)
        {
            valoresInventario1[i] = "";
        }
    }

    // ✅ CORREGIDO: Método para usar objetos
    public void UsarItem(string objeto)
    {
        Debug.Log("🩹 [Inventario1] Intentando usar objeto: " + objeto);

        if (vidasPlayer == null)
        {
            Debug.LogError("❌ [Inventario1] No se encontró referencia a VidasPlayer.");
            return;
        }

        switch (objeto)
        {
            case "PCV":
                if (numPCV > 0)
                {
                    vidasPlayer.Curar(1); // Cura 1 punto de vida
                    numPCV--;
                    Debug.Log("💚 [Inventario1] Usó PCV, curación aplicada. Restan: " + numPCV);
                    
                    // Actualizar la UI
                    if (numPCV == 0) 
                    {
                        EliminaDelInventario("PCV");
                    }
                    ActualizaBotones();
                }
                else
                {
                    Debug.LogWarning("⚠️ [Inventario1] No hay PCV disponibles para usar");
                }
                break;

            case "PCM":
                // Aquí puedes agregar lógica para el mana
                Debug.Log("🔵 [Inventario1] Usó PCM (poción de mana)");
                break;

            default:
                Debug.Log($"ℹ️ [Inventario1] El objeto {objeto} no es usable o no tiene función definida");
                break;
        }
    }

    private void EliminaDelInventario(string objeto)
    {
        int pos = VerificaEnArreglo(objeto);
        if (pos != -1)
        {
            valoresInventario1[pos] = "";
        }
    }
}