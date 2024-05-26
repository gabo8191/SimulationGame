using UnityEngine;

/*
 * Este script controla el comportamiento de los tres primeros jefes del juego.
 */

public class Boss1 : MonoBehaviour
{
    /*
     * moveSpeed: velocidad de movimiento del jefe.
     * patrolDistance: distancia que recorre el jefe.
     * originalX: posición original del jefe.
     * currentTarget: posición a la que se dirige el jefe.
     * movingRight: indica si el jefe se mueve a la derecha.
     * attackRange: rango de ataque del jefe.
     * playerInRange: indica si el jugador está en rango de ataque.
     * combatBoss: componente CombatBoss del jefe.
     * animator: componente Animator del jefe.
     * healthComponent: componente Health del jefe.
     * attackCooldown: tiempo de espera entre ataques.
     * lastAttackTime: tiempo del último ataque.
     */
    public float moveSpeed = 2f;
    public float patrolDistance = 5f;
    private float originalX;
    private float currentTarget;
    private bool movingRight = true;
    public float attackRange = 3f;
    private bool playerInRange = false;
    private CombatBoss combatBoss;
    private Animator animator;
    private Health healthComponent;

    public float attackCooldown = 3f;
    private float lastAttackTime = 0;


    /*
     * Este método se llama al inicio del juego, se encarga de inicializar las variables originalX y currentTarget.
     * El health component se inicializa si es nulo.
     */
    private void Start()
    {
        originalX = transform.position.x;
        currentTarget = originalX + patrolDistance;
        animator = GetComponent<Animator>();
        combatBoss = GetComponent<CombatBoss>();
        if (healthComponent == null)
        {
            healthComponent = gameObject.AddComponent<Health>();
        }
    }

    /*
     *Este método se llama en cada frame, se encarga de mover al jefe y de atacar al jugador si está en rango.
     */
    private void Update()
    {
        Patrol();
        if (playerInRange && Time.time > lastAttackTime + attackCooldown)
        {
            AttackPlayer();
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    /*
     *Este método se encarga de mover al jefe de un punto a otro.
     *Si el jefe llega al punto de destino, cambia de dirección.
     */

    private void Patrol()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentTarget, transform.position.y), step);

        if (Mathf.Abs(transform.position.x - currentTarget) < 0.01f)
        {
            currentTarget = movingRight ? originalX - patrolDistance : originalX + patrolDistance;
            movingRight = !movingRight;
            transform.localScale = new Vector3(movingRight ? 2.8321f : -2.8321f, 2.284f, 1);
        }
    }

    /*
     *Este método se encarga de atacar al jugador.
     *El jefe ejecuta un ataque y se actualiza el tiempo del último ataque.
     */

    private void AttackPlayer()
    {
        animator.SetBool("isAttacking", true);
        combatBoss.ExecuteAttack();
        lastAttackTime = Time.time;
    }

    /*
     *Este método se llama cuando el jugador entra en el rango de ataque del jefe.
     *Si el jugador está en rango, playerInRange se establece en true.
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Vector2.Distance(transform.position, collision.transform.position) <= attackRange)
            playerInRange = true;
    }

    /*
     *Este método se llama cuando el jugador sale del rango de ataque del jefe.
     *Si el jugador sale del rango, playerInRange se establece en false.
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerInRange = false;
    }

    /*
     *Este método se llama cuando el jefe recibe daño.
     *El jefe recibe daño y se comprueba si ha muerto.
     */

    public void TomarDaño(float daño)
    {
        healthComponent.TakeDamage(10);
    }

    /*
     *Este método se llama cuando el jefe muere.
     *El jefe se destruye.
     */

    private void Muerte()
    {
        Destroy(gameObject);
    }
}
