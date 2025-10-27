using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparaProyectil : MonoBehaviour
{
    private SpriteRenderer spriteBala;
    [SerializeField] private float velocidad = 12f;
    private ManaPlayer manaPlayer; // Referencia al sistema de mana
    
    void Start()
    {
        // Buscar el sistema de mana automáticamente
        manaPlayer = FindObjectOfType<ManaPlayer>();
        if (manaPlayer == null)
        {
            Debug.LogError(" No se encontró ManaPlayer en la escena");
        }

        // Debug 1: Indica que se generó la bala con más información
        Debug.Log("¡BALA GENERADA! - Objeto: " + gameObject.name + 
                 " en posición: " + transform.position +
                 " - Dirección de disparo: " + CAD.dirDistaparo +
                 " - Activo: " + gameObject.activeInHierarchy);
                 
        // CONSUMO DE MANA AL CREAR EL PROYECTIL
        ConsumirManaPorDisparo();
    }
    
    private void ConsumirManaPorDisparo()
    {
        if (manaPlayer != null)
        {
            int costoDisparo = 10; // Puedes ajustar este valor
            bool manaConsumido = manaPlayer.GastarMana(costoDisparo);
            
            if (!manaConsumido)
            {
                Debug.LogWarning(" Proyectil creado pero sin mana suficiente - podría destruirse");
                // Opcional: Destruir el proyectil si no hay mana
                // Destroy(this.gameObject);
            }
        }
        else
        {
            Debug.LogWarning(" No se encontró ManaPlayer - el disparo no consume mana");
        }
    }
    
    void FixedUpdate()
    {
        // Debug del movimiento en cada frame (solo unos segundos para no saturar)
        if (Time.time < 2f) // Solo muestra los primeros 2 segundos
        {
            Debug.Log("Bala moviéndose - Posición: " + transform.position + 
                     " - Dirección: " + CAD.dirDistaparo);
        }
        
        //Calculos de posiciones
        if (CAD.dirDistaparo == 1)
        {
            //Se agrega el vector, y se hacen los calculos de velocidad (no se toca la z)
            transform.position += new Vector3(0, -1, 0) * Time.deltaTime * velocidad;
        }
        else if (CAD.dirDistaparo == 2)
        {
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * velocidad;
        }
        else if (CAD.dirDistaparo == 3)
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            //spriteBala.flipX = true;
        }
        else if (CAD.dirDistaparo == 4)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * velocidad;
        }
        //Recordar agregar disparos en diagonal
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        // Debug 2: Indica colisión
        Debug.Log("BALA COLISIONÓ con: " + collision.gameObject.name + " - TAG: " + collision.gameObject.tag);
        
        //Depende donde choque se destruye la bala y o se hace daño
        if (collision.gameObject.tag == "Limites")
        {
            Debug.Log("Bala destruida por Límites");
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Enemigo")
        {
            Debug.Log("Bala impactó en ENEMIGO - Aplicando daño");
            collision.transform.GetComponent<Enemigo>().TomarDaño(1);
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("BALA DESTRUIDA: " + gameObject.name);
    }
}