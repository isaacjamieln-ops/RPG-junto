using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    // 🩸 Vida del enemigo (editable desde el Inspector)
    [SerializeField] private int vida = 3;

    // ⚔️ Control de ataque
    [SerializeField] private float freAtaque = 2.5f;
    private float tiempoSigAtaque = 0f;
    private float iniciaConteo = 0f;

    // 👁️ Detección y movimiento
    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer = 5f;
    private bool estaPatrullando = true;
    [SerializeField] private float distanciaMinimaPunto = 0.5f;

    // 🧭 Visuales y animación
    private SpriteRenderer spriteEnemigo;
    private Transform mirarHacia;
    private Animator anim;

    // 💰 Recompensa
    [SerializeField] private GameObject recompensaPrefab;
    [SerializeField] private Transform puntoDrop;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // ✅ No reasignamos vida (ahora puedes editarla desde el Inspector)
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        // ✅ Inicia patrulla si hay puntos configurados
        if (puntosRuta.Length > 0)
        {
            agente.SetDestination(puntosRuta[indiceRuta].position);
            mirarHacia = puntosRuta[indiceRuta];
        }
    }

    void Update()
    {
        // 🔹 Mantiene al enemigo en el eje Z=0
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        float distanciaAlJugador = Vector3.Distance(personaje.position, transform.position);
        bool jugadorEnRangoAnterior = playerEnRango;
        playerEnRango = distanciaAlJugador < distanciaDeteccionPlayer;

        // 🔄 Si el jugador sale del rango, retoma la patrulla
        if (jugadorEnRangoAnterior && !playerEnRango)
        {
            estaPatrullando = true;
            if (puntosRuta.Length > 0)
            {
                indiceRuta = ObtenerPuntoMasCercano();
                agente.SetDestination(puntosRuta[indiceRuta].position);
                mirarHacia = puntosRuta[indiceRuta];
            }
        }

        // 🔹 Movimiento principal
        if (playerEnRango)
        {
            estaPatrullando = false;
            agente.SetDestination(personaje.position);
            mirarHacia = personaje;
        }
        else if (estaPatrullando && puntosRuta.Length > 0)
        {
            Patrullar();
        }

        // 🔁 Rotación visual
        RotaEnemigo();

        // ⏱️ Control de tiempo entre ataques
        if (tiempoSigAtaque > 0)
            tiempoSigAtaque = freAtaque + iniciaConteo - Time.time;
        else
        {
            tiempoSigAtaque = 0;
            VidasPlayer.perderVida = 1;
        }
    }

    // 🔹 Patrullaje automático entre puntos
    private void Patrullar()
    {
        float distanciaAlPunto = Vector3.Distance(transform.position, puntosRuta[indiceRuta].position);

        if (distanciaAlPunto <= distanciaMinimaPunto)
        {
            indiceRuta++;
            if (indiceRuta >= puntosRuta.Length)
                indiceRuta = 0;

            agente.SetDestination(puntosRuta[indiceRuta].position);
            mirarHacia = puntosRuta[indiceRuta];
        }
    }

    // 🔹 Encuentra el punto de patrulla más cercano
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

    // 🔹 Cambia la orientación visual del enemigo
    private void RotaEnemigo()
    {
        if (mirarHacia != null)
        {
            spriteEnemigo.flipX = transform.position.x > mirarHacia.position.x;
        }
    }

    // ⚔️ Detección de colisión con el jugador
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            // Evita ataques múltiples instantáneos
            if (Time.time >= iniciaConteo + freAtaque)
            {
                VidasPlayer vidas = obj.GetComponent<VidasPlayer>();
                if (vidas != null)
                {
                    vidas.TomarDaño(1);
                    iniciaConteo = Time.time;
                }
            }
        }
    }

    // 🩸 Recibir daño
    public void TomarDaño(int daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Morir();
        }
    }

    // 💀 Muerte del enemigo
    private void Morir()
    {
        Debug.Log("El enemigo ha muerto.");

        // Generar recompensa si existe
        if (recompensaPrefab != null)
        {
            Vector3 posicionDrop = puntoDrop != null ? puntoDrop.position : transform.position;
            Instantiate(recompensaPrefab, posicionDrop, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
