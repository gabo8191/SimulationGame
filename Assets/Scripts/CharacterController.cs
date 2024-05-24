using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
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

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        animator = GetComponent<Animator>();

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

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

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

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

    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.1f);

        while (!EstaEnSuelo())
        {
            yield return null;
        }

        puedeMoverse = true;
    }

    public void ActivarImpulso()
    {
        StartCoroutine(AumentarVelocidadPorTiempo(2f, 5f)); // Multiplica velocidad por 2 durante 5 segundos
    }

    IEnumerator AumentarVelocidadPorTiempo(float factor, float duracion)
    {
        impulsoActivo = true;
        yield return new WaitForSeconds(duracion);
        impulsoActivo = false;
    }

    public void ActivarSalto()
    {
        StartCoroutine(AumentarFuerzaSaltoPorTiempo(2f, 5f)); // Multiplica fuerza de salto por 2 durante 5 segundos
    }

    IEnumerator AumentarFuerzaSaltoPorTiempo(float factor, float duracion)
    {
        fuerzaSalto *= factor;
        yield return new WaitForSeconds(duracion);
        fuerzaSalto /= factor;
    }

    public void AumentarVida()
    {
        StartCoroutine(RecuperarVida()); // Recuperar una vida
    }

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
