using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class movCamara : MonoBehaviour
{
    public Camera camara;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "portal1")
        {
            Vector3 posicioncamara = new Vector3(56.1f, 85.8f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(55.9f, 82.1f, 0);
            this.transform.position = posicionPlayer;
        }

        if (obj.gameObject.tag == "portal1r")
        {
            Vector3 posicioncamara = new Vector3(1.8f, 30.3f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(1.5f, 31.7f, 0);
            this.transform.position = posicionPlayer;
        }
        // if(obj.gameObject.tag == "portal1")
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        // }

        if (obj.gameObject.tag == "portal2")
        {
            Vector3 posicioncamara = new Vector3(-68.5f, 80.1f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(-68.6f, 72.2f, 0);
            this.transform.position = posicionPlayer;
        }

        if (obj.gameObject.tag == "portal2r")
        {
            Vector3 posicioncamara = new Vector3 (53.1f, 106.4f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(53.6f, 110.2F, 0);
            this.transform.position = posicionPlayer;
        }
        if (obj.gameObject.tag == "portal3")
        {
            Vector3 posicioncamara = new Vector3 (-131.9f, -13.7f, 0);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3 (-131.7f, -14.1f, -10);
            this.transform.position = posicionPlayer;
        }
        if (obj.gameObject.tag == "portal3r")
        {
            Vector3 posicioncamara = new Vector3 (-49.4f, 106.9f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(-46.5f, 106.7f, 0);
            this.transform.position = posicionPlayer;
        }
        if (obj.gameObject.tag == "portal4")
        {
            Vector3 posicioncamara = new Vector3  (-225.5f, 78.51f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3 (-224.9f, 79.1f, 0);
            this.transform.position = posicionPlayer;
        }
        if (obj.gameObject.tag == "portal4r")
        {
            Vector3 posicioncamara = new Vector3 (-129.5f, 27.3f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3 (-129.5f, 26.9f, 0);
            this.transform.position = posicionPlayer;
        }

    }
}
