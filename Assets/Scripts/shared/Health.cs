using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;  // Inicializa la salud al máximo al comenzar
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private void Die()
    {
        // Lógica de muerte, como animaciones o efectos
        EnemyDrops drops = GetComponent<EnemyDrops>();
        if (drops != null)
        {
            drops.DropItem();  // Llama a DropItem para generar el drop
        }
        Destroy(gameObject);  // Destruye el objeto
    }

}
