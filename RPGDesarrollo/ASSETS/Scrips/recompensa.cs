using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recompensa : MonoBehaviour
{
    [SerializeField] private GameObject efecto;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Sumar puntos
            if (puntaje != null)
            {
                puntaje.SumarPuntos(cantidadPuntos);
            }
            else
            {
                Debug.LogWarning("No se asignó el script de Puntaje al objeto de recompensa.");
            }

            // Crear efecto
            if (efecto != null)
            {
                Instantiate(efecto, transform.position, Quaternion.identity);
            }

            // Destruir la recompensa
            Destroy(gameObject);
        }
    }
}
