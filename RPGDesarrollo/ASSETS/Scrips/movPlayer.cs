using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movPlayer : MonoBehaviour
{

    private Vector2 dirMovimiento;
    public float velMovimiento;
    public Rigidbody2D rb;
    public Animator anim;
    //Variable con la que se mueve el árbol de animaciones
    public static int dirAtaque = 0; //1.-Front , 2.- Back, 3.-Left 4.-Right

    [SerializeField] private string capaIde;
    [SerializeField] private string capaCaminar; 
    private bool PlayerMoviendose = false;
    private float ultimoMovX, ultimoMovY;
    // Permite realizar calculos cuadro a cuadro
    void FixedUpdate()
    {
        Movimiento();
        AnimacionesMago();
        if(CCC.atacando == false && CAD.disparando == false)
        {
            AnimacionesMago();
        }
    }

    private void Movimiento()
    {
        float movimientoX = Input.GetAxisRaw("Horizontal");
        float movimientoY = Input.GetAxisRaw("Vertical");
        dirMovimiento = new Vector2(movimientoX, movimientoY).normalized;
        rb.velocity = new Vector2(dirMovimiento.x * velMovimiento,
        dirMovimiento.y * velMovimiento);

        //Condicionales de movimiento
        if (movimientoX == -1)
        {
            dirAtaque = 3;
        }
        if (movimientoX == 1)
        {
            dirAtaque = 4;
        }
        if (movimientoY == -1)
        {
            dirAtaque = 1;
        }
        if(movimientoY == 1)
        {
            dirAtaque = 2;
        }

        if (movimientoX == 0 && movimientoY == 0)//idle
        {
            PlayerMoviendose = false;
        }
        else //caminar
        {
            PlayerMoviendose = true;
            ultimoMovX = movimientoX;
            ultimoMovY = movimientoY;

        }

        ActualizarCapa();

    }
    private void AnimacionesMago()
    {
        anim.SetFloat("movimientoX", ultimoMovX);
        anim.SetFloat("movimientoY", ultimoMovY);
    }
    private void ActualizarCapa()
    {
        /* if (PlayerMoviendose)
         {
             activaCapa(capaCaminar);
             Debug.Log("Camianar");
         }
         else
         {
             activaCapa(capaIde);
             Debug.Log("ide");

         }*/
        if (CCC.atacando == false && CAD.disparando == false)
        {//Llamada de las clases de ataque
            if (PlayerMoviendose)
            {
                activaCapa("Caminar");
            }
            else
            {
                activaCapa("ide");
            }
        }
        else
        {
            activaCapa("Ataque");
        }
    }
    
    //llamar a capa correspondiente
    private void activaCapa(string nombre)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);//Ambos layer los mandamos a 0
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);//solo se encendera al que se le de el peso, se swichearan la opcion
    }
    
}
