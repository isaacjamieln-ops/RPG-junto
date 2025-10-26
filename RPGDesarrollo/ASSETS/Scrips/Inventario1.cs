using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario1 : MonoBehaviour
{
    private bool muestraInventario1;
    public GameObject goInventario1;

    [SerializeField] private string[] valoresInventario1; // Arreglo de objetos en el inventario

    // Contadores de objetos acumulables
    private int numPCV, numPCM, numMPV, numMPM, numMO, numEX, numGE, numPEZ;

    // Referencias a botones
    private Button[] botonesInventario1;

    // Sprites de objetos
    public Sprite PCV, PCM, MPV, MPM, EM, BS, FM, BA, AVI, AM, AV, AAL, MO, EX, GE, PEZ, contenedor;

    void Start()
    {
        // ⚠️ Verificación de referencia
        if (goInventario1 == null)
            Debug.LogError("⚠️ [Inventario1] goInventario1 no está asignado en el inspector. Asigna el panel de inventario.");

        muestraInventario1 = false;
        BorraArreglo();

        // Inicializar contadores
        numPCV = numPCM = numMPV = numMPM = numMO = numEX = numGE = numPEZ = 0;

        // Obtener todos los botones del panel de inventario
        if (goInventario1 != null)
            botonesInventario1 = goInventario1.GetComponentsInChildren<Button>(true);
        else
            botonesInventario1 = new Button[0];
    }

    public void StatusInventario()
    {
        if (goInventario1 == null) return; // Evita error si no está asignado

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

    private void ActualizaBotones()
    {
        if (botonesInventario1 == null || botonesInventario1.Length == 0) return;

        for (int i = 0; i < valoresInventario1.Length && i < botonesInventario1.Length; i++)
        {
            string objeto = valoresInventario1[i];
            if (string.IsNullOrEmpty(objeto))
            {
                botonesInventario1[i].GetComponent<Image>().sprite = contenedor;
                botonesInventario1[i].GetComponentInChildren<Text>().text = "";
            }
            else
            {
                DibujaElementos(i);
            }
        }
    }

    public void EscribeEnArreglo(string objeto)
    {
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

            // Objetos únicos — solo se muestran una vez
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
        boton.GetComponentInChildren<Text>().text = texto;
    }

    private void BorraArreglo()
    {
        for (int i = 0; i < valoresInventario1.Length; i++)
        {
            valoresInventario1[i] = "";
        }
    }
}
