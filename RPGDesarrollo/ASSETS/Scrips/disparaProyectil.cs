using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparaProyectil : MonoBehaviour
{
    [SerializeField]private float velocidad = 0.8f;
    void FixedUpdate()
    {
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
        }
        else if (CAD.dirDistaparo == 4)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * velocidad;
        }
        //Recordar agregar disparos en diagonal
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { //Depende donde choque se destruye la bala y o se hace daño
        if (collision.gameObject.tag == "limites")
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "enemigo")
        {
            collision.transform.GetComponent<Enemigo>().TomarDaño(1);
            Destroy(this.gameObject);
        }
    }
}
