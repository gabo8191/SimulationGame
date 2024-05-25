using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad;
    public bool debePerseguir;
    public float distancia; // Qu� tan lejos est� el enemigo del objetivo
    public float distanciaAbsoluta;
    private Animator animator;
    public GameObject dropPrefab; // Prefab del objeto que se soltará al morir
    private Health healthComponent;
    private EnemyDrops enemyDropsComponent;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        healthComponent = gameObject.GetComponent<Health>();
        if (healthComponent == null)
        {
            healthComponent = gameObject.AddComponent<Health>();
        }

        // Configurar la salud máxima, por ejemplo
        healthComponent.maxHealth = 100;

        // Añadir el componente EnemyDrops
        enemyDropsComponent = gameObject.GetComponent<EnemyDrops>();
        if (enemyDropsComponent == null)
        {
            enemyDropsComponent = gameObject.AddComponent<EnemyDrops>();
            // Configurar drops aquí, por ejemplo:
            enemyDropsComponent.possibleDrops = new List<GameObject>(); // Agregar objetos de drop
        }
    }

    // Update is called once per frame
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            debePerseguir = false;

            rb.velocity = new Vector2(0, 0);

            //QUITAR VIDA AL JUGADOR


            GameManager.Instance.RestarVida(1);

        }
    }

    public void TomarDaño(float daño)
    {
        Debug.Log("Toma daño");
        healthComponent.TakeDamage(daño);
    }

    private void Muerte()
    {
        enemyDropsComponent.DropItem();
        Destroy(gameObject);
    }
}
