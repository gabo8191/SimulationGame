using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * *Este script controla el comportamiento de los objetos que permiten al jugador saltar. Esto por medio de los drops que entregan la habilidad de salto doble por parte del enemigo.
 * */
public class DropJump : MonoBehaviour
{
    /*
     * Este método se llama cuando el jugador colisiona con el objeto que le permite saltar.
     * Si el jugador colisiona con el objeto, se activa la habilidad de salto doble y se destruye el objeto.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterController>().ActivarSalto();
            Destroy(gameObject);
        }
    }
}
