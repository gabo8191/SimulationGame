using System.Collections;
using UnityEngine;

/*
 *Este script controla el comportamiento del jefe final.
 */

public class FinalBoss : MonoBehaviour
{
    /*
     * arrowPrefab: Prefab del proyectil que dispara el jefe final.
     * shootingPoint: Punto de origen del proyectil.
     * shootingInterval: Intervalo de tiempo entre disparos.
     * shootingTimer: Temporizador para controlar el intervalo de tiempo entre disparos.
     * playerInRange: Bandera que indica si el jugador está en rango de ataque.
     * player: Transform del jugador.
     * startPosition: Posición inicial del jefe final.
     * endPosition: Posición final del jefe final.
     * moveTimer: Temporizador para controlar el movimiento del jefe final.
     * moveDuration: Duración del movimiento del jefe final.
     * healthComponent: Componente Health del jefe final.
     */
     
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

    /*
     *Este método se llama al inicio del juego, se encarga de inicializar las posiciones de inicio y fin del jefe final.
     *Además, inicializa el temporizador de disparo y obtiene el componente Health.
     */

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

    /*
     *Este método se llama en cada frame, se encarga de rotar el jefe final hacia el jugador y de controlar el disparo.
     */
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

    /*
     *Este método se llama cuando el jugador entra en el rango de ataque del jefe final.
     */

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            playerInRange = true;
        }
    }

    /*
     *Este método se llama cuando el jugador sale del rango de ataque del jefe final.
     */

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            playerInRange = false;
        }
    }

    /*
     *Este método se llama cuando el jefe final recibe daño.
     *Toma 10 de daño.
     */

    public void TomarDaño(float daño)
    {
        healthComponent.TakeDamage(10);
    }

    /*
     *Este método se llama cuando el jefe final muere.
     *Se destruye el jefe final.
     */

    private void Muerte()
    {
        Destroy(gameObject);
    }

    /*
     *Este método se encarga de controlar el disparo del jefe final.
     *Si el temporizador de disparo llega a 0 y el jugador está en rango, se dispara una flecha.
     */

    void HandleShooting()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && playerInRange)
        {
            ShootArrow();
            shootingTimer = shootingInterval;
        }
    }

    /*
     *Este método se encarga de disparar una flecha.
     */

    void ShootArrow()
    {
        if (arrowPrefab && shootingPoint)
        {
            GameObject arrow = Instantiate(arrowPrefab, shootingPoint.position, shootingPoint.rotation);
        }
    }
}
