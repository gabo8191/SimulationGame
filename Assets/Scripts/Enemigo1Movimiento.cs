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
        public float distancia; // Qu� tan lejos est� el enemigo del objetivo
        public float distanciaAbsoluta;


        void Start()
        {
            // Inicializamos el contador con el tiempo configurado para cambiar de direcci�n
            contadorT = tiempoParaCambiar;

            // Inicializamos la semilla con un valor arbitrario
            seed = Random.Range(0f, m);

            // Invocamos el m�todo saltar a intervalos regulares
            InvokeRepeating("Saltar", 2f, 3f);

            // Desactiva la rotaci�n del Rigidbody2D
            rbd.freezeRotation = true;

        }

        void Update()
        {
            // Calculamos la distancia entre el enemigo y el objetivo
            distanciaAbsoluta = Vector2.Distance(transform.position, objetivo.position);

            // Si el objetivo est� lo suficientemente cerca, perseguirlo
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
                // Si el objetivo est� lejos, seguir movi�ndose pseudoaleatoriamente
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

            // Verificamos si es momento de evaluar un cambio de direcci�n
            if (contadorT <= 0)
            {
                // Reiniciamos el contador
                contadorT = tiempoParaCambiar;

                // Generamos un n�mero pseudoaleatorio
                seed = LinearCongruentialGenerator(seed, a, c, m);

                // Determinamos la nueva direcci�n bas�ndonos en el n�mero generado
                esDerecha = (seed / m) < 0.5f;
            }

            // Aseguramos que la rotaci�n del enemigo sea siempre cero en el eje Z
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        void Saltar()
        {
            // A�adimos una fuerza hacia arriba para simular el salto
            rbd.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

            // Opcional: cambiar de direcci�n despu�s de un salto
            seed = LinearCongruentialGenerator(seed, a, c, m);
            esDerecha = (seed / m) < 0.5f;
        }

        // M�todo de congruencia lineal para generar n�meros pseudoaleatorios
        float LinearCongruentialGenerator(float seed, float a, float c, float m)
        {
            return (a * seed + c) % m;
        }

    //CONFIGURAR LA GRAVEDAD


        //QUITAR VIDA AL JUGADOR
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.RestarVida(1);
            }

            //si el enemigo se encuentra con Ground cambia de direccion
            if (collision.gameObject.CompareTag("Ground"))
        {
                esDerecha = !esDerecha;
            }


        }
    }

