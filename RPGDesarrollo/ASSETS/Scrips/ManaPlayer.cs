using UnityEngine;
using UnityEngine.UI;

public class ManaPlayer : MonoBehaviour
{
    public Image manaPlayer;
    private float anchoManaPlayer;
    public static int mana;
    private const int manaINI = 100;
    public int costoDisparo = 15;

    void Start()
    {
        if (manaPlayer == null)
        {
            return;
        }

        anchoManaPlayer = manaPlayer.rectTransform.rect.width;
        mana = manaINI;
        DibujaMana();
    }

    public bool GastarMana(int cantidad)
    {
        if (mana >= cantidad)
        {
            mana -= cantidad;
            if (mana < 0) mana = 0;

            DibujaMana();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RecuperarMana(int cantidad)
    {
        int manaAntes = mana;
        mana += cantidad;
        if (mana > manaINI) mana = manaINI;

        DibujaMana();
    }

    public void DibujaMana()
    {
        if (manaPlayer == null) return;
        
        RectTransform transformImagen = manaPlayer.rectTransform;
        float nuevaAnchura = anchoManaPlayer * (float)mana / (float)manaINI;
        transformImagen.sizeDelta = new Vector2(nuevaAnchura, transformImagen.sizeDelta.y);
    }

    public bool TieneManaSuficiente(int cantidad)
    {
        return mana >= cantidad;
    }

    public void RestaurarManaCompleto()
    {
        mana = manaINI;
        DibujaMana();
    }
}