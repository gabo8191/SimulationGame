using UnityEngine;

/*
 * Este script controla la vida de los enemigos.
 */
public class Health : MonoBehaviour
{
    /*
     * maxHealth: vida máxima del enemigo.
     * currentHealth: vida actual del enemigo.
     */
    public float maxHealth;
    public float currentHealth;

    /*
     *Este método se llama al inicio del juego, se encarga de asignar la vida actual del enemigo.
     */
    void Start()
    {
        currentHealth = maxHealth;
    }

    /*
     *Este método se llama cuando el enemigo recibe daño.
     *Si la vida del enemigo es menor o igual a 0, el enemigo muere.
     */

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /*
     *Este método se llama cuando el enemigo recibe curación.
     *Si la vida del enemigo es mayor a la vida máxima, la vida del enemigo se iguala a la vida máxima.
     */

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    /*
     *Este método se llama cuando el enemigo muere.
     *Si el enemigo muere, se destruye el enemigo.
     */
    private void Die()
    {
        EnemyDrops drops = GetComponent<EnemyDrops>();
        if (drops != null)
        {
            drops.DropItem();
        }
        Destroy(gameObject);
    }

}
