using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CAD : MonoBehaviour
{

    [SerializeField] private GameObject proyectil;//Agregar imagen
    public float tiempoSigAtaque;
    public float timepoEntreAtaques;
    public Transform puntoEmision;
    private Animator anim; //animacion
    public static int dirDistaparo = 0;
    public static bool disparando = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (tiempoSigAtaque < 0.05f && timepoEntreAtaques > 0)
        {
            disparando = false;
        }
        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire2") && tiempoSigAtaque <= 0)
        {
            disparando = true;
            activaCapa("Ataque");
            Dispara();
            tiempoSigAtaque = timepoEntreAtaques;
        }

    }

    private void Dispara() //Condicionales de direccion de Golpe
    {
        if (movPlayer.dirAtaque == 1) {
            anim.SetTrigger("ataque-abajo");
        } else if (movPlayer.dirAtaque == 2) {
            anim.SetTrigger("ataque-arriba");
        } else if (movPlayer.dirAtaque == 3) {
            anim.SetTrigger("ataque-izquierda");
        } else if (movPlayer.dirAtaque == 4) {
            anim.SetTrigger("ataque-derecha");
        }
    }

    private void EmiteProyectil()
    {
        dirDistaparo = movPlayer.dirAtaque;
        Instantiate(proyectil, puntoEmision.position, transform.rotation);

    }
    
        private void activaCapa(string nombre)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); //Ambos layers con weight en 0
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }
}
