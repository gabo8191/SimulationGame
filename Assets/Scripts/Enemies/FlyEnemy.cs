using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Este script controla el comportamiento de los enemigos voladores.
 */
public class FlyEnemy : MonoBehaviour
{
    /*
     * objetivo: Transform que representa el objetivo al que el enemigo debe perseguir.
     * velocidad: float que representa la velocidad a la que se mueve el enemigo.
     * debePerseguir: bool que indica si el enemigo debe perseguir al objetivo.
     * distancia: float que representa la distancia entre el enemigo y el objetivo.
     * distanciaAbsoluta: float que representa la distancia absoluta entre el enemigo y el objetivo.
     * animator: Animator que controla la animación del enemigo.
     * dropPrefab: GameObject que representa el prefab del drop que el enemigo puede soltar.
     * healthComponent: Health que controla la vida del enemigo.
     * enemyDropsComponent: EnemyDrops que controla los drops del enemigo.
     * rb: Rigidbody2D que controla el Rigidbody del enemigo.
     */

    public Transform objetivo;
    public float velocidad;
    public bool debePerseguir;
    public float distancia;
    public float distanciaAbsoluta;
    private Animator animator;
    public GameObject dropPrefab;
    private Health healthComponent;
    private EnemyDrops enemyDropsComponent;
    private Rigidbody2D rb;


    /*
     * Este método se llama al inicio del juego, se encarga de inicializar el Rigidbody y los componentes Health y EnemyDrops.
     */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        healthComponent = gameObject.GetComponent<Health>();
        if (healthComponent == null)
        {
            healthComponent = gameObject.AddComponent<Health>();
        }
        healthComponent.maxHealth = 100;
        enemyDropsComponent = gameObject.GetComponent<EnemyDrops>();
        if (enemyDropsComponent == null)
        {
            enemyDropsComponent = gameObject.AddComponent<EnemyDrops>();
            enemyDropsComponent.possibleDrops = new List<GameObject>(); 
        }
    }

    /*
     * Este método se llama en cada frame, se encarga de mover al enemigo hacia el objetivo y de controlar la dirección del enemigo.
     */

    void Update()
    {
        distancia = objetivo.position.x - transform.position.x;
        distanciaAbsoluta = Mathf.Abs(distancia);

        if (debePerseguir)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
        }

        if (distancia > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (distancia < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (distanciaAbsoluta < 3)
        {
            debePerseguir = true;
        }
        else
        {
            debePerseguir = false;
        }
    }

    /*
     * Este método se llama cuando el enemigo colisiona con otro objeto.
     * Si el objeto con el que colisiona es el jugador, se resta una vida al jugador y se detiene el movimiento del enemigo.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            debePerseguir = false;
            rb.velocity = new Vector2(0, 0);
            GameManager.Instance.RestarVida(1);
        }
    }

    /*
     * Este método se llama cuando el enemigo toma daño.
     * Si la vida del enemigo es menor o igual a 0, se ejecuta el método Muerte.
     */

    public void TomarDaño(float daño)
    {
        Debug.Log("Toma daño");
        healthComponent.TakeDamage(daño);
    }

    /*
     * Este método se llama cuando el enemigo muere.
     * Se destruye el enemigo y se ejecuta el método DropItem.
     */

    private void Muerte()
    {
        enemyDropsComponent.DropItem();
        Destroy(gameObject);
    }
}
