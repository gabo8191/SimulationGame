using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Este script controla el comportamiento de las monedas.
 */

public class Coins : MonoBehaviour
{
    
    /*
     *valor: valor de la moneda.
     *gameManager: referencia al GameManager.
     *sonidoMoneda: sonido que se reproduce al recoger una moneda.
     */

    public int valor = 1;
    public GameManager gameManager;
    public AudioClip sonidoMoneda;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /*
     *Este método se llama cuando el jugador colisiona con la moneda.
     *Si el jugador colisiona con la moneda, se suma el valor de la moneda a los puntos del jugador.
     *Se destruye la moneda y se reproduce el sonido de la moneda.
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameManager.SumarPuntos(valor);

            Destroy(gameObject);
            AudioManager.Instance.ReproducirSonido(sonidoMoneda);
        }
    }


}
