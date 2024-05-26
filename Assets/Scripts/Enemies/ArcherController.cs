using System.Collections;
using UnityEngine;

/*
 *Este script controla el comportamiento del arquero.
 */

public class ArcherController : MonoBehaviour
{
    /*
     * moveSpeed: Velocidad de movimiento del arquero.
     * arrowPrefab: Prefab del proyectil que dispara el arquero.
     * shootingPoint: Punto de origen del proyectil.
     * shootingInterval: Intervalo de tiempo entre disparos.
     * playerInRange: Bandera que indica si el jugador está en rango de ataque.
     * player: Transform del jugador.
     * startPosition: Posición inicial del arquero.
     * endPosition: Posición final del arquero.
     * moveTimer: Temporizador para controlar el movimiento del arquero.
     * moveDuration: Duración del movimiento del arquero.
     * healthComponent: Componente Health del arquero.
     */

    public float moveSpeed = 3.0f;
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
     *Este método se llama al inicio del juego, se encarga de inicializar las posiciones de inicio y fin del arquero.
     *además, inicializa el temporizador de disparo y obtiene el componente Health.
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
            healthComponent.maxHealth = 100;
        }
    }

    /*
     *Este método se llama en cada frame, se encarga de rotar el arquero hacia el jugador y de controlar el disparo.
     *Además, se encarga de mover al arquero de un punto a otro.
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
        MoveBackAndForth();
    }

    /*
     *Este método se llama cuando el jugador entra en el rango de ataque del arquero.
     */

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            playerInRange = true;
        }
    }

    /*
     *Este método se llama cuando el jugador sale del rango de ataque del arquero.
     */

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            playerInRange = false;
        }
    }

    /*
     *Este método se encarga de hacer que el arquero tome daño.
     */

    public void TomarDaño(float daño)
    {
        healthComponent.TakeDamage(10);
    }

    /*
     *Este método se encarga de destruir al arquero.
     */

    private void Muerte()
    {
        Destroy(gameObject);
    }

    /*
    *Este método se encarga de mover al arquero de un punto a otro.
    */
    void MoveBackAndForth()
    {
        moveTimer += Time.deltaTime;
        float t = Mathf.PingPong(moveTimer, moveDuration) / moveDuration;
        transform.position = Vector3.Lerp(startPosition, endPosition, t);
    }

    /*
     *Este método se encarga de controlar el disparo del arquero.
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
