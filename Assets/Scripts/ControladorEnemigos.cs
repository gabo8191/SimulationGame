using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControladorEnemigos : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    private float tiempoEntreEnemigos;  // Este valor ahora será dinámico

    private float tiempoSiguienteEnemigo;
    private float seed;  // Semilla para el generador de números pseudoaleatorios
    private bool generarEnemigos = false; // Controla si los enemigos deben generarse

    void Start()
    {
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);

        seed = RandomGenerator.GetInitialSeed(); // Inicializar semilla para el generador
    }

    void Update()
    {
        if (generarEnemigos)
        {
            tiempoSiguienteEnemigo += Time.deltaTime;

            if (tiempoSiguienteEnemigo >= tiempoEntreEnemigos)
            {
                tiempoSiguienteEnemigo = 0;
                CrearEnemigo();
                SetNextEnemyTime();  // Establecer el siguiente tiempo de aparición después de crear un enemigo
            }
        }
    }

    private void CrearEnemigo()
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length);
        Vector2 posicionAleatoria = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        Instantiate(enemigos[numeroEnemigo], posicionAleatoria, Quaternion.identity);
    }

    private void SetNextEnemyTime()
    {
        seed = RandomGenerator.Generate(seed); // Actualizar semilla
        tiempoEntreEnemigos = (seed % 10) + 1; // Tiempo entre 1 y 10 segundos
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            generarEnemigos = true; // Comenzar a generar enemigos
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            generarEnemigos = false; // Detener la generación de enemigos
        }
    }

}
