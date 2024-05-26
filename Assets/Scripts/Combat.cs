    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /*
     *Este script controla el combate del personaje principal.
     */
    public class Combat : MonoBehaviour
    {

        /*
         * controladorGolpe: referencia al controlador del golpe.
         *radioGolpe: radio del golpe.
         *dañoGolpe: daño del golpe.
         *seed: semilla para el cálculo del daño.
         *animator: componente Animator del personaje.                                       
         */

        [SerializeField] private Transform controladorGolpe;
        [SerializeField] private float radioGolpe;
        private float dañoGolpe;
        private float seed;
        public Animator animator;
        public AudioClip audioSource;

    /*
     *Este método se llama al inicio del juego, se encarga de obtener el componente Animator del personaje y de generar una semilla aleatoria.
    */
        private void Start()
        {
            animator = GetComponent<Animator>();
            seed = RandomGenerator.GetInitialSeed();
        }
        
    /*
     *Este método se llama en cada frame, se encarga de detectar si se ha pulsado la tecla Q para golpear.
     */   

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(Golpear());
            }
        }

    /*
     Este método se encarga de golpear a los enemigos que se encuentren en el radio de golpe.
     */

    private IEnumerator Golpear()
    {
        animator.SetTrigger("Golpear");
        AudioManager.Instance.ReproducirSonido(audioSource);
        yield return null;
        Golpe();
    }


    /*
     *Este método se encarga de calcular el daño del golpe utilizando el método de Monte Carlo.
     *El método de Monte Carlo se utiliza para aproximar el valor esperado de una variable aleatoria.
     *Se generan números aleatorios y se calcula el daño del golpe en función de estos números.
    */
    private void Golpe()
        {
            dañoGolpe = CalcularDañoMonteCarlo();
            Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

            foreach (Collider2D colisionador in objetos)
            {
                if (colisionador.CompareTag("Enemy"))
                {
                    colisionador.GetComponent<Health>().TakeDamage(dañoGolpe);
                }
            }
        }


    /*
     * Este método se encarga de calcular el daño del golpe utilizando el método de Monte Carlo. 
     */
        private float CalcularDañoMonteCarlo()
        {
            seed = RandomGenerator.Generate(seed);
            float rand = seed / 4294967296f;

            if (rand < 0.5)
            {
                return Random.Range(10, 16);
            }
            else if (rand < 0.8)
            {
                return Random.Range(16, 26);
            }
            else
            {
                return Random.Range(26, 36);
            }
        }

    /*
     * Gizmos es una herramienta de Unity que permite visualizar información en la escena.
     */

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
        }
    }
