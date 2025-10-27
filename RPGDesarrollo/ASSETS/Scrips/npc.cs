using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
public GameObject txtDialogo;
public int numVisitas;
    public Sprite txt1, txt2, txt3; //Cantidad de texto a eleccion
    void Start()
    {
        txtDialogo.SetActive(false);
        numVisitas = 0;
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        { 
            txtDialogo.SetActive(true);
            EscribeDialogo();
            numVisitas++;
        }
    }
    private void EscribeDialogo() { //Cambio o de dialogo N veses
        SpriteRenderer sr = txtDialogo.GetComponent<SpriteRenderer>();
        switch (numVisitas)
        {
            case 0:
                sr.sprite = txt1;
                sr.flipX = true;
                break;
            case 1:
                sr.sprite = txt2;
                sr.flipX = true;
                break;
            case 2:
                sr.sprite = txt3;
                sr.flipX = true;
                break;
            
        }
    }

}
