using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1Movimiento : MonoBehaviour
{
    // Variables públicas configurables desde el Inspector
    public float speed;  // Velocidad del enemigo
    public float tiempoParaCambiar;  // Tiempo entre posibles cambios de dirección
    public float fuerzaSalto;  // Fuerza del salto

    // Variables privadas para el control interno
    private float contadorT;
    private float seed;  // Semilla para el generador de números pseudoaleatorios

    // Parámetros para el método de congruencia lineal
    private const float a = 1664525f;
    private const float c = 1013904223f;
    private const float m = 4294967296f;

    private bool esDerecha;
    public Rigidbody2D rbd;


    public Transform objetivo;
    public float distancia; // Qué tan lejos está el enemigo del objetivo
    public float distanciaAbsoluta;


    void Start()
    {
        // Inicializamos el contador con el tiempo configurado para cambiar de dirección
        contadorT = tiempoParaCambiar;

        // Inicializamos la semilla con un valor arbitrario
        seed = Random.Range(0f, m);

        // Invocamos el método saltar a intervalos regulares
        InvokeRepeating("Saltar", 2f, 3f);

        // Desactiva la rotación del Rigidbody2D
        rbd.freezeRotation = true;
    }

    void Update()
    {
        // Calculamos la distancia entre el enemigo y el objetivo
        distanciaAbsoluta = Vector2.Distance(transform.position, objetivo.position);

        // Si el objetivo está lo suficientemente cerca, perseguirlo
        if (distanciaAbsoluta < distancia)
        {
            // Movimiento del enemigo hacia el objetivo
            if (transform.position.x < objetivo.position.x)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.localScale = new Vector3(-1, 1, 1);
                esDerecha = true;
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                transform.localScale = new Vector3(1, 1, 1);
                esDerecha = false;
            }
        }
        else
        {
            // Si el objetivo está lejos, seguir moviéndose pseudoaleatoriamente
            if (esDerecha)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        // Decrementamos el contador de tiempo
        contadorT -= Time.deltaTime;

        // Verificamos si es momento de evaluar un cambio de dirección
        if (contadorT <= 0)
        {
            // Reiniciamos el contador
            contadorT = tiempoParaCambiar;

            // Generamos un número pseudoaleatorio
            seed = LinearCongruentialGenerator(seed, a, c, m);

            // Determinamos la nueva dirección basándonos en el número generado
            esDerecha = (seed / m) < 0.5f;
        }

        // Aseguramos que la rotación del enemigo sea siempre cero en el eje Z
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    void Saltar()
    {
        // Añadimos una fuerza hacia arriba para simular el salto
        rbd.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

        // Opcional: cambiar de dirección después de un salto
        seed = LinearCongruentialGenerator(seed, a, c, m);
        esDerecha = (seed / m) < 0.5f;
    }

    // Método de congruencia lineal para generar números pseudoaleatorios
    float LinearCongruentialGenerator(float seed, float a, float c, float m)
    {
        return (a * seed + c) % m;
    }

    //QUITAR VIDA AL JUGADOR
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.RestarVida(1);
        }
    }
}
