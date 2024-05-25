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
    private Animator animator;  // Referencia al Animator

    private void Start()
    {
        originalX = transform.position.x;
        currentTarget = originalX + patrolDistance;
        animator = GetComponent<Animator>();
        combatBoss = GetComponent<CombatBoss>();
    }

    private void Update()
    {
        Patrol();
        if (playerInRange)
        {
            AttackPlayer();
        }
        else
        {
            animator.SetBool("isAttacking", false);  // Asegurarse de que no esté atacando si el jugador no está en rango
        }
    }

    private void Patrol()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentTarget, transform.position.y), step);

        if (Mathf.Abs(transform.position.x - currentTarget) < 0.01f)
        {
            if (currentTarget == originalX + patrolDistance)
            {
                currentTarget = originalX - patrolDistance;
                movingRight = false;  // Cambia la dirección a la izquierda
            }
            else
            {
                currentTarget = originalX + patrolDistance;
                movingRight = true;  // Cambia la dirección a la derecha
            }
        }

        // Cambiar la dirección de la imagen basada en la dirección de movimiento
        // Asegurarse de mantener el mismo tamaño estableciendo la escala Y y Z a 1
        if (movingRight)
            transform.localScale = new Vector3(2.8321f, 2.284f, 1);  // Orientación normal
        else
            transform.localScale = new Vector3(-2.8321f, 2.284f, 1);  // Invertir horizontalmente
    }


    private void AttackPlayer()
    {
        animator.SetBool("isAttacking", true);  // Activar la animación de ataque
        combatBoss.ExecuteAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Vector2.Distance(transform.position, collision.transform.position) <= attackRange)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
