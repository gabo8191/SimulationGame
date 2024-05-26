    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

/*
 * Este script controla el movimiento del enemigo 1 (Green)
 */

    public class Enemigo1Movimiento : MonoBehaviour
    {
        /*
         * speed: velocidad del enemigo.
         * tiempoParaCambiar: tiempo que tarda en cambiar de dirección.
         * fuerzaSalto: fuerza con la que salta el enemigo.
         * contadorT: contador de tiempo.
         *seed: semilla para el generador de números aleatorios.
         * a: constante para el generador de números aleatorios.
         * c: constante para el generador de números aleatorios.
         * m: constante para el generador de números aleatorios.
         * esDerecha: indica si el enemigo se mueve a la derecha.
         * rbd: referencia al componente Rigidbody2D.
         * objetivo: objetivo al que se dirige el enemigo.
         * distancia: distancia a la que se encuentra el objetivo.
         * distanciaAbsoluta: distancia absoluta entre el enemigo y el objetivo.
         * healthComponent: referencia al componente Health.
         */


        public float speed;
        public float tiempoParaCambiar;
        public float fuerzaSalto;

        private float contadorT;
        private float seed;

        private const float a = 1664525f;
        private const float c = 1013904223f;
        private const float m = 4294967296f;

        private bool esDerecha;
        public Rigidbody2D rbd;


        public Transform objetivo;
        public float distancia;
        public float distanciaAbsoluta;

        private Health healthComponent;


    /*
     * Este método se llama al inicio del juego, se encarga de inicializar las variables contadorT y seed.
     */

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

    /*
     * Este método se llama en cada frame, se encarga de mover al enemigo y de cambiar de dirección.
     * También se encarga de saltar cada cierto tiempo.
     * Además, se encarga de rotar al enemigo
     */

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

    /*
    * Este método se encarga de hacer saltar al enemigo.
    */

        void Saltar()
        {
            rbd.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

            seed = LinearCongruentialGenerator(seed, a, c, m);
            esDerecha = (seed / m) < 0.5f;
        }

    /*
     * Generador de números pseudoaleatorios por el método de congruencia lineal.
     */

        float LinearCongruentialGenerator(float seed, float a, float c, float m)
        {
            return (a * seed + c) % m;
        }

    /*
     * Cuando el enemigo colisiona con el jugador, el jugador pierde una vida.
     * Si el enemigo colisiona con el suelo, cambia de dirección.
     */

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

        /*
         * Este método se llama cuando el enemigo colisiona con un proyectil.
         * Si el enemigo colisiona con un proyectil, el enemigo recibe daño.
         */

        public void TomarDaño(float daño)
        {
            healthComponent.TakeDamage(10);
        }
    
    }

