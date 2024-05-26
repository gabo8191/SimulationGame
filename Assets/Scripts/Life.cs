using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Este script controla el comportamiento de los objetos que permiten al jugador recuperar vida.
 */

public class Life : MonoBehaviour
{

    /*
     *Este método se llama cuando el jugador colisiona con el objeto que le permite recuperar vida.
     *Si el jugador colisiona con el objeto, se aumenta la vida del jugador y se destruye el objeto.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool vidaRecuperada = GameManager.Instance.RecuperarVida();
            if (vidaRecuperada)
            {
                Destroy(gameObject);
            }
        }
    }
}
