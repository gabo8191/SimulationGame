using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Este script controla la c�mara del juego.
 */

public class CameraController : MonoBehaviour
{
    /*
     *personaje: referencia al personaje principal.
     *tama�oCamara: tama�o de la c�mara o recuadro de la c�mara.
     *alturaPantalla: altura de la pantalla para calcular la posici�n de la c�mara.
     *                 */
    public Transform personaje;
    private float tama�oCamara;
    private float alturaPantalla;

    /*
     *Este m�todo se llama al inicio del juego, se encarga de obtener el tama�o de la c�mara y la altura de la pantalla.
     *La altura de la pantalla se calcula multiplicando el tama�o de la c�mara por 2 es necesario porque la c�mara se encuentra en la posici�n 0,0,0.
     */

    void Start()
    {
        tama�oCamara = Camera.main.orthographicSize;
        alturaPantalla = tama�oCamara * 2;
    }

    /*
     *Este m�todo se llama en cada frame, se encarga de calcular la posici�n de la c�mara.
     *La posici�n de la c�mara se calcula teniendo en cuenta la posici�n del personaje.
     *Va dentro del update para que se actualice en cada frame.
     */

    void Update()
    {
        CalcularPosicionCamara();
    }

    /*
     *Este m�todo se encarga de calcular la posici�n de la c�mara.
     *La posici�n de la c�mara se calcula teniendo en cuenta la posici�n del personaje.
     */

    private void CalcularPosicionCamara()
    {
        int pantallaPersonaje = (int)(personaje.position.y / alturaPantalla);
        float alturaCamara = (pantallaPersonaje * alturaPantalla) + tama�oCamara;
        transform.position = new Vector3(transform.position.x, alturaCamara, transform.position.z);
    }
}
