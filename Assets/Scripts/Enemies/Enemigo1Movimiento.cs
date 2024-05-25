using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1Movimiento : MonoBehaviour
{
    // Variables p�blicas configurables desde el Inspector
    public float speed;  // Velocidad del enemigo
    public float tiempoParaCambiar;  // Tiempo entre posibles cambios de direcci�n
    public float fuerzaSalto;  // Fuerza del salto

    // Variables privadas para el control interno
    private float contadorT;
    private float seed;  // Semilla para el generador de n�meros pseudoaleatorios

    // Par�metros para el m�todo de congruencia lineal
    private const float a = 1664525f;
    private const float c = 1013904223f;
    private const float m = 4294967296f;

    private bool esDerecha;
    public Rigidbody2D rbd;


    public Transform objetivo;
    public float distancia;
    public float distanciaAbsoluta;

    private Health healthComponent;

    void Start()
    {
        contadorT = tiempoParaCambiar;

        seed = Random.Range(0f, m);

        InvokeRepeating(nameof(Saltar), 2f, 3f);

        rbd = GetComponent<Rigidbody2D>();

        rbd.freezeRotation = true;

        healthComponent = gameObject.GetComponent<Health>();
        if (healthComponent == null)
        {
            healthComponent = gameObject.AddComponent<Health>();
            healthComponent.maxHealth = 100;
        }

    }

    void Update()
    {
        distanciaAbsoluta = Vector2.Distance(transform.position, objetivo.position);

        if (distanciaAbsoluta < distancia)
        {
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

        contadorT -= Time.deltaTime;

        if (contadorT <= 0)
        {
            contadorT = tiempoParaCambiar;

            seed = LinearCongruentialGenerator(seed, a, c, m);

            esDerecha = (seed / m) < 0.5f;
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    void Saltar()
    {
        rbd.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

        seed = LinearCongruentialGenerator(seed, a, c, m);
        esDerecha = (seed / m) < 0.5f;
    }

    float LinearCongruentialGenerator(float seed, float a, float c, float m)
    {
        return (a * seed + c) % m;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.RestarVida(1);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (esDerecha)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }

    public void TomarDaño(float daño)
    {
        healthComponent.TakeDamage(10);
    }
    
}

