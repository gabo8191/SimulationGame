using System.Collections;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootingPoint;
    public float shootingInterval = 2.0f;
    private float shootingTimer;
    private bool playerInRange = false;
    public Transform player;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float moveTimer;
    private float moveDuration = 1.0f;
    private Health healthComponent;

    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x + 5, startPosition.y, startPosition.z);
        shootingTimer = shootingInterval;
        healthComponent = gameObject.GetComponent<Health>();
        if (healthComponent == null)
        {
            healthComponent = gameObject.AddComponent<Health>();
        }
    }

    void Update()
    {
        if (playerInRange)
        {
            Vector3 directionToPlayer = (player.position - shootingPoint.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            shootingPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            HandleShooting();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            playerInRange = false;
        }
    }

    public void TomarDaño(float daño)
    {
        healthComponent.TakeDamage(10);
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }

    void HandleShooting()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && playerInRange)
        {
            ShootArrow();
            shootingTimer = shootingInterval;
        }
    }

    void ShootArrow()
    {
        if (arrowPrefab && shootingPoint)
        {
            GameObject arrow = Instantiate(arrowPrefab, shootingPoint.position, shootingPoint.rotation);
            // Assumir que el prefab de la flecha ya tiene el script Arrow, donde se maneja el movimiento y la autodestrucción.
        }
    }
}
