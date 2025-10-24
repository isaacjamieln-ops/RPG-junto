using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CCC : MonoBehaviour
{
    public Transform controladorGolpe; //posicion del controlador
    public float radioGolpe;
    public int dañoGolpe;
    public float timepoEntreAtaques;
    public float tiempoSigAtaque;
    private Animator anim;

    public static bool atacando;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (tiempoSigAtaque < 0.05f && timepoEntreAtaques > 0)
        {
            atacando = false;
        }
        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque -= Time.deltaTime;
        }//Precionar boton para activar disparo
        if (Input.GetButtonDown("Fire1") && tiempoSigAtaque <= 0)
        {
            atacando = true;
            activaCapa("Ataque");
            Golpe();
            tiempoSigAtaque = timepoEntreAtaques;
        }
    }

private void Golpe()
    {
        if (movPlayer.dirAtaque == 1)
        {
            anim.SetTrigger("ataqueFront");
        } else if (movPlayer.dirAtaque == 2)
        {
            anim.SetTrigger("ataqueBack");
        } else if (movPlayer.dirAtaque == 3)
        {
            anim.SetTrigger("ataqueLeft");
        } else if (movPlayer.dirAtaque == 4)
        {
            anim.SetTrigger("ataqueRight");
        }
    }


    private void verificaGolpe()//Evento agregado a la Animacion
    {
        //Verificar golpe
        Collider2D[] objs = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objs)
        { //Revisar que enemigo se esta tocado
            if (colisionador.CompareTag("Enemigo"))//Daño solo si pega al enemigo
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OawGizmos() //sin referencia
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

    private void activaCapa(string nombre)//marcador del controlador
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); //Ambos layers con weight en 0
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }
    
}
