using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public static int vidaEnemigo = 1;
    private float freAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false; // Bandera de rango
    [SerializeField] private float distanciaDeteccionPlayer; // Manipulable desde Unity
    private SpriteRenderer spriteEnemigo;
    private Transform mirarHacia; // Puntos de interés

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        vidaEnemigo = 1;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }

    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        float distancia = Vector3.Distance(personaje.position, this.transform.position);
        // Movimiento entre puntos de ruta
        
        
        if (this.transform.position == puntosRuta[indiceRuta].position)
        {
            if (indiceRuta <= puntosRuta.Length - 1)
            {
                indiceRuta++;
            }
            else if (indiceRuta == puntosRuta.Length - 1)
            {
                indiceRuta = 0;
            }
        }

        // Detección del jugador
        if (distancia < distanciaDeteccionPlayer)
        {
            playerEnRango = true;
        }
        else
        {
            playerEnRango = false;
        }

        // Frecuencia de ataque
        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque = freAtaque + iniciaConteo - Time.time;
        }
        else
        {
            tiempoSigAtaque = 0;
            VidasPlayer.perderVida = 1;
            SiguePlayer(playerEnRango);
            RotaEnemigo();
        }
    }

    // 🔹 MÉTODO FUERA DE UPDATE()
    private void SiguePlayer(bool playerEnRango)
    {
        if (playerEnRango)
        {
            agente.SetDestination(personaje.position);
            mirarHacia = personaje;
        }
        else
        {
            agente.SetDestination(puntosRuta[indiceRuta].position);
            mirarHacia = puntosRuta[indiceRuta];
        }
    }

    private void RotaEnemigo()
    {
        if (this.transform.position.x > mirarHacia.position.x)
        {
            spriteEnemigo.flipX = true;
            Debug.Log("FlipX");
        }
        else
        {
            spriteEnemigo.flipX = false;
            Debug.Log("Sin FlipX");
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            tiempoSigAtaque = freAtaque;
            iniciaConteo = Time.time;
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDaño(1);
        }
    }

    public void TomarDaño(int daño)
    {
        vidaEnemigo -= daño;
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}