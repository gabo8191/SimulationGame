using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Este script controla el comportamiento de los objetos que permiten al jugador recuperar velocidad.
 */
public class DropSpeed : MonoBehaviour
{

    /*
     * Este método se llama cuando el jugador colisiona con el objeto que le permite recuperar velocidad.
     * Si el jugador colisiona con el objeto, se activa la habilidad de impulso y se destruye el objeto.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterController>().ActivarImpulso();
            Destroy(gameObject);
        }
    }
}
