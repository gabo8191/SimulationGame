using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Este script controla la cámara del juego.
 */

public class CameraController : MonoBehaviour
{
    /*
     *personaje: referencia al personaje principal.
     *tamañoCamara: tamaño de la cámara o recuadro de la cámara.
     *alturaPantalla: altura de la pantalla para calcular la posición de la cámara.
     *                 */
    public Transform personaje;
    private float tamañoCamara;
    private float alturaPantalla;

    /*
     *Este método se llama al inicio del juego, se encarga de obtener el tamaño de la cámara y la altura de la pantalla.
     *La altura de la pantalla se calcula multiplicando el tamaño de la cámara por 2 es necesario porque la cámara se encuentra en la posición 0,0,0.
     */

    void Start()
    {
        tamañoCamara = Camera.main.orthographicSize;
        alturaPantalla = tamañoCamara * 2;
    }

    /*
     *Este método se llama en cada frame, se encarga de calcular la posición de la cámara.
     *La posición de la cámara se calcula teniendo en cuenta la posición del personaje.
     *Va dentro del update para que se actualice en cada frame.
     */

    void Update()
    {
        CalcularPosicionCamara();
    }

    /*
     *Este método se encarga de calcular la posición de la cámara.
     *La posición de la cámara se calcula teniendo en cuenta la posición del personaje.
     */

    private void CalcularPosicionCamara()
    {
        int pantallaPersonaje = (int)(personaje.position.y / alturaPantalla);
        float alturaCamara = (pantallaPersonaje * alturaPantalla) + tamañoCamara;
        transform.position = new Vector3(transform.position.x, alturaCamara, transform.position.z);
    }
}
