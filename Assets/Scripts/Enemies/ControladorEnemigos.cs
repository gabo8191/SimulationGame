using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 *Este script controla la generación de enemigos en el juego.
 */
public class ControladorEnemigos : MonoBehaviour
{
    /*
     * minX: posición mínima en el eje X.
     * maxX: posición máxima en el eje X.
     * minY: posición mínima en el eje Y.
     * maxY: posición máxima en el eje Y.
     * puntos: arreglo de puntos de generación de enemigos.
     * enemigos: arreglo de enemigos.
     * tiempoEntreEnemigos: tiempo entre la generación de enemigos.
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
     *Este método se llama al inicio del juego, se encarga de obtener las posiciones mínimas y máximas en los ejes X y Y.
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
     *Este método se llama en cada frame, se encarga de generar enemigos si la bandera generarEnemigos es verdadera.
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
     *Este método se encarga de crear un enemigo en una posición aleatoria.
     */

    private void CrearEnemigo()
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length);
        Vector2 posicionAleatoria = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        Instantiate(enemigos[numeroEnemigo], posicionAleatoria, Quaternion.identity);
    }

    /*
     *Este método se encarga de generar un tiempo aleatorio para la generación de enemigos.
     */

    private void SetNextEnemyTime()
    {
        seed = RandomGenerator.Generate(seed);
        tiempoEntreEnemigos = (seed % 10) + 1;
    }

    /*
     *Este método se llama cuando el jugador entra en el área de generación de enemigos.
     *Si el jugador entra en el área, se activa la bandera generarEnemigos.
     */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            generarEnemigos = true;
        }
    }

    /*
     *Este método se llama cuando el jugador sale del área de generación de enemigos.
     *Si el jugador sale del área, se desactiva la bandera generarEnemigos.
     */

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            generarEnemigos = false;
        }
    }

}
