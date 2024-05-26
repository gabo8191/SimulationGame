using System.Collections;
using UnityEngine;

/*
 * Este script controla el comportamiento del personaje principal.
 */
public class CharacterController : MonoBehaviour
{
    /*
     * velocidad: velocidad del personaje.
     * fuerzaSalto: fuerza con la que salta el personaje.
     * fuerzaGolpe: fuerza del golpe.
     * saltosMaximos: cantidad m�xima de saltos que puede realizar el personaje.
     * capaSuelo: capa del suelo, necesario para detectar si el personaje est� en el suelo.
     * sonidoSalto: sonido que se reproduce al saltar.
     * 
     * rigidBody: componente Rigidbody2D del personaje.
     * boxCollider: componente BoxCollider2D del personaje.
     * mirandoDerecha: indica si el personaje est� mirando a la derecha.
     * saltosRestantes: cantidad de saltos que le quedan al personaje.
     * animator: componente Animator del personaje.
     * puedeMoverse: indica si el personaje puede moverse.
     * impulsoActivo: indica si el impulso est� activo.
     *    
     *    Instance: instancia de la clase CharacterController.
     */

    public float velocidad;
    public float fuerzaSalto;
    public float fuerzaGolpe;
    public float saltosMaximos;
    public LayerMask capaSuelo;
    public AudioClip sonidoSalto;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private float saltosRestantes;
    private Animator animator;
    private bool puedeMoverse = true;
    private bool impulsoActivo = false;

    public static CharacterController Instance;

    /*
     * Este m�todo se llama al inicio del juego, se encarga de obtener los componentes Rigidbody2D y BoxCollider2D del personaje.
     * Adem�s, inicializa la cantidad de saltos restantes y obtiene el componente Animator.
     */

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        animator = GetComponent<Animator>();

        Instance = this;
    }

    /*
     * Este m�todo se llama en cada frame, se encarga de procesar el movimiento y el salto del personaje.
     */

    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    /*
     *Este m�todo se encarga de detectar si el personaje est� en el suelo.
     *Devuelve true si el personaje est� en el suelo, de lo contrario, devuelve false.
     */

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    /*
     * Este m�todo se encarga de procesar el salto del personaje.
     * El personaje puede saltar si est� en el suelo o si le quedan saltos restantes.
     * Si el personaje salta, se reproduce el sonido de salto.
     */
    void ProcesarSalto()
    {
        if (EstaEnSuelo())
        {
            saltosRestantes = saltosMaximos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * (impulsoActivo ? fuerzaSalto * 2 : fuerzaSalto), ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(sonidoSalto);
        }
    }

    /*
     *Este m�todo se encarga de procesar el movimiento del personaje.
     *El personaje se mueve en el eje horizontal seg�n la entrada del jugador.
     *Si el personaje se mueve, se activa la animaci�n de correr.
     *Si el personaje no se mueve, se desactiva la animaci�n de correr.
     *Si el personaje recibe un golpe, se desactiva la posibilidad de moverse durante un tiempo.
     *Si el impulso est� activo, la velocidad del personaje se duplica.
     *Si el personaje cambia de direcci�n, se invierte la escala en el eje x.
     */
    void ProcesarMovimiento()
    {
        if (!puedeMoverse) return;

        float inputMovimiento = Input.GetAxis("Horizontal");

        if (inputMovimiento != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        rigidBody.velocity = new Vector2(inputMovimiento * (impulsoActivo ? velocidad * 2 : velocidad), rigidBody.velocity.y);

        GestionarOrientacion(inputMovimiento);
    }

    /*
     *Este m�todo se encarga de gestionar la orientaci�n del personaje.
     *Si el personaje est� mirando a la derecha y la entrada del jugador es negativa, o si el personaje est� mirando a la izquierda y la entrada del jugador es positiva, se invierte la escala en el eje x.
     */

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    /*
     *Este m�todo se encarga de aplicar un golpe al personaje.
     *El personaje no puede moverse durante un tiempo despu�s de recibir un golpe.
     *El personaje recibe un impulso en la direcci�n opuesta a la que se mueve.
     *Despu�s de un tiempo, el personaje puede moverse de nuevo.
     */

    public void AplicarGolpe()
    {
        puedeMoverse = false;
        Vector2 direccionGolpe;
        if (rigidBody.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else
        {
            direccionGolpe = new Vector2(1, 1);
        }
        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);
        StartCoroutine(EsperarYActivarMovimiento());
    }

    /*
     *Este m�todo se encarga de esperar un tiempo y activar la posibilidad de moverse.
     */

    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.1f);

        while (!EstaEnSuelo())
        {
            yield return null;
        }
        puedeMoverse = true;
    }

    /*
     *Este m�todo se encarga de activar el impulso del personaje.
     *El personaje aumenta su velocidad durante un tiempo.
     */
    public void ActivarImpulso()
    {
        StartCoroutine(AumentarVelocidadPorTiempo(2f, 5f));
    }

    /*
     * Este m�todo se encarga de aumentar la velocidad del personaje durante un tiempo.
     */

    IEnumerator AumentarVelocidadPorTiempo(float factor, float duracion)
    {
        impulsoActivo = true;
        yield return new WaitForSeconds(duracion);
        impulsoActivo = false;
    }

    /*
     *Este m�todo se encarga de activar el salto del personaje.
     *El personaje aumenta la fuerza de salto durante un tiempo.
     */

    public void ActivarSalto()
    {
        StartCoroutine(AumentarFuerzaSaltoPorTiempo(2f, 5f));
    }

    /*
     *Este m�todo se encarga de aumentar la fuerza de salto del personaje durante un tiempo.
     */

    IEnumerator AumentarFuerzaSaltoPorTiempo(float factor, float duracion)
    {
        fuerzaSalto *= factor;
        yield return new WaitForSeconds(duracion);
        fuerzaSalto /= factor;
    }

    /*
     *Este m�todo se encarga de aumentar la vida del personaje. 
     *Por medio de una corrutina, que es un m�todo que se ejecuta en paralelo con el resto del c�digo.
     */

    public void AumentarVida()
    {
        StartCoroutine(RecuperarVida());
    }

    /*
     *ste m�todo se encarga de recuperar la vida del personaje.
     */
    IEnumerator RecuperarVida()
    {
        bool vidaRecuperada = GameManager.Instance.RecuperarVida();
        if (!vidaRecuperada)
        {
            yield break;
        }
        yield return null;
    }


}
