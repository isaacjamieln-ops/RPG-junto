using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    //public static int vidaEnemigo = 1;
    private float freAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer;
    private SpriteRenderer spriteEnemigo;
    private Transform mirarHacia;
    private Animator anim;

    //  NUEVO: Variables para mejorar la patrulla
    [SerializeField] private float distanciaMinimaPunto = 0.5f;
    [SerializeField] private int vidaEnemigo;
    private bool estaPatrullando = true;

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
        
        // INICIAR PATRULLA AUTOMÁTICAMENTE
        if (puntosRuta.Length > 0)
        {
            agente.SetDestination(puntosRuta[indiceRuta].position);
            mirarHacia = puntosRuta[indiceRuta];
        }
    }

    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        float distanciaAlJugador = Vector3.Distance(personaje.position, this.transform.position);

        // DETECCIÓN MEJORADA DEL JUGADOR
        bool jugadorEnRangoAnterior = playerEnRango;
        playerEnRango = distanciaAlJugador < distanciaDeteccionPlayer;

        // SI EL JUGADOR SALE DEL RANGO, VOLVER A LA PATRULLA
        if (jugadorEnRangoAnterior && !playerEnRango)
        {
            estaPatrullando = true;
            if (puntosRuta.Length > 0)
            {
                // Volver al punto de ruta más cercano
                indiceRuta = ObtenerPuntoMasCercano();
                agente.SetDestination(puntosRuta[indiceRuta].position);
                mirarHacia = puntosRuta[indiceRuta];
            }
        }

        // LÓGICA PRINCIPAL DE MOVIMIENTO
        if (playerEnRango)
        {
            // SEGUIR AL JUGADOR
            estaPatrullando = false;
            agente.SetDestination(personaje.position);
            mirarHacia = personaje;
        }
        else if (estaPatrullando && puntosRuta.Length > 0)
        {
            // PATRULLAR ENTRE PUNTOS
            float distanciaAlPunto = Vector3.Distance(transform.position, puntosRuta[indiceRuta].position);
            
            if (distanciaAlPunto <= distanciaMinimaPunto)
            {
                // AVANZAR AL SIGUIENTE PUNTO
                indiceRuta++;
                if (indiceRuta >= puntosRuta.Length)
                {
                    indiceRuta = 0;
                }
                agente.SetDestination(puntosRuta[indiceRuta].position);
                mirarHacia = puntosRuta[indiceRuta];
            }
        }

        // ROTACIÓN (SIEMPRE ACTIVA)
        RotaEnemigo();

        // SECCIÓN DEL DAÑO (SIN MODIFICACIONES)
        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque = freAtaque + iniciaConteo - Time.time;
        }
        else
        {
            tiempoSigAtaque = 0;
            VidasPlayer.perderVida = 1;
        }
    }

    // NUEVO MÉTODO: Encontrar el punto de ruta más cercano
    private int ObtenerPuntoMasCercano()
    {
        int puntoMasCercano = 0;
        float distanciaMinima = Mathf.Infinity;
        
        for (int i = 0; i < puntosRuta.Length; i++)
        {
            float distancia = Vector3.Distance(transform.position, puntosRuta[i].position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                puntoMasCercano = i;
            }
        }
        return puntoMasCercano;
    }

    // MÉTODO DE SEGUIMIENTO (SIMPLIFICADO)
    private void SiguePlayer(bool playerEnRango)
    {
        if (playerEnRango)
        {
            agente.SetDestination(personaje.position);
            mirarHacia = personaje;
        }
    }

    private void RotaEnemigo()
    {
        if (mirarHacia != null)
        {
            if (this.transform.position.x > mirarHacia.position.x)
            {
                spriteEnemigo.flipX = true;
            }
            else
            {
                spriteEnemigo.flipX = false;
            }
        }
    }

    // SECCIÓN DEL DAÑO (SIN MODIFICACIONES)
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