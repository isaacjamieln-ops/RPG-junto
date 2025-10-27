using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private int cantidadMana = 20; // Cuánto mana recupera este ítem

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Buscar el componente ManaPlayer del jugador
            ManaPlayer manaPlayer = other.GetComponent<ManaPlayer>();

            if (manaPlayer != null)
            {
                manaPlayer.RecuperarMana(cantidadMana);
                Debug.Log($" Ítem de mana recogido: +{cantidadMana} de mana");
            }
            else
            {
                Debug.LogWarning(" El jugador no tiene componente ManaPlayer");
            }

            // Destruir el ítem una vez recogido
            Destroy(this.gameObject);
        }
    }
}
