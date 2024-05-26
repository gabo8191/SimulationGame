using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 *Este script controla la generaci�n de enemigos en el juego.
 */
public class ControladorEnemigos : MonoBehaviour
{
    /*
     * minX: posici�n m�nima en el eje X.
     * maxX: posici�n m�xima en el eje X.
     * minY: posici�n m�nima en el eje Y.
     * maxY: posici�n m�xima en el eje Y.
     * puntos: arreglo de puntos de generaci�n de enemigos.
     * enemigos: arreglo de enemigos.
     * tiempoEntreEnemigos: tiempo entre la generaci�n de enemigos.
     * tiempoSiguienteEnemigo: tiempo para generar el siguiente enemigo.
     * seed: semilla para generar el tiempo entre enemigos.
     * generarEnemigos: bandera para generar enemigos.
     */

    private float minX, maxX, minY, maxY;
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    private float tiempoEntreEnemigos;

    private float tiempoSiguienteEnemigo;
    private float seed;
    private bool generarEnemigos = false;


    /*
     *Este m�todo se llama al inicio del juego, se encarga de obtener las posiciones m�nimas y m�ximas en los ejes X y Y.
     */
    void Start()
    {
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);

        seed = RandomGenerator.GetInitialSeed();
    }

    /*
     *Este m�todo se llama en cada frame, se encarga de generar enemigos si la bandera generarEnemigos es verdadera.
     */
    void Update()
    {
        if (generarEnemigos)
        {
            tiempoSiguienteEnemigo += Time.deltaTime;

            if (tiempoSiguienteEnemigo >= tiempoEntreEnemigos)
            {
                tiempoSiguienteEnemigo = 0;
                CrearEnemigo();
                SetNextEnemyTime();
            }
        }
    }

    /*
     *Este m�todo se encarga de crear un enemigo en una posici�n aleatoria.
     */

    private void CrearEnemigo()
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length);
        Vector2 posicionAleatoria = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        Instantiate(enemigos[numeroEnemigo], posicionAleatoria, Quaternion.identity);
    }

    /*
     *Este m�todo se encarga de generar un tiempo aleatorio para la generaci�n de enemigos.
     */

    private void SetNextEnemyTime()
    {
        seed = RandomGenerator.Generate(seed);
        tiempoEntreEnemigos = (seed % 10) + 1;
    }

    /*
     *Este m�todo se llama cuando el jugador entra en el �rea de generaci�n de enemigos.
     *Si el jugador entra en el �rea, se activa la bandera generarEnemigos.
     */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            generarEnemigos = true;
        }
    }

    /*
     *Este m�todo se llama cuando el jugador sale del �rea de generaci�n de enemigos.
     *Si el jugador sale del �rea, se desactiva la bandera generarEnemigos.
     */

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            generarEnemigos = false;
        }
    }

}
