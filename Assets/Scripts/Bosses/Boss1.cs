using UnityEngine;

public class Boss1 : MonoBehaviour
{
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

    public float attackCooldown = 3f;  // Tiempo entre ataques en segundos
    private float lastAttackTime = 0;  // Cuándo ocurrió el último ataque

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

    private void AttackPlayer()
    {
        animator.SetBool("isAttacking", true);
        combatBoss.ExecuteAttack();
        lastAttackTime = Time.time;  // Actualizar el tiempo del último ataque
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Vector2.Distance(transform.position, collision.transform.position) <= attackRange)
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerInRange = false;
    }

    public void TomarDaño(float daño)
    {
        healthComponent.TakeDamage(10);
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }
}
