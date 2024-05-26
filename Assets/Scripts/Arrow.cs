using UnityEngine;

/*
 * Este script controla el comportamiento de las flechas que disparan los enemigos. 
 */

public class Arrow : MonoBehaviour
{
    /*
     * lifetime: tiempo de vida de la flecha.
     * speed: velocidad de la flecha.
     * player: referencia al jugador.
     * target: posici�n a la que se dirige la flecha.
     */
    public float lifetime = 5.0f;
    public float speed;
    private Transform player;
    private Vector2 target;

    /*
     * Este m�todo se llama al inicio del juego, se encarga de obtener la referencia al jugador 
     * y la posici�n a la que se debe dirigir la flecha teniendo en cuenta la posici�n del jugador.
     */
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    /*
     *Este m�todo se llama en cada frame, se encarga de mover la flecha hacia la posici�n target.
     *Si la flecha llega a la posici�n target, se destruye.
    */

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyProyectile();
        }
    }

    /*
     *Este m�todo se llama cuando la flecha colisiona con otro objeto.
     *Si el objeto con el que colisiona es el jugador, se resta una vida al jugador y se destruye la flecha.
     */

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                GameManager.Instance.RestarVida(1);
            }
            DestroyProyectile();
        }
    }
    
    /*
     *Este m�todo se encarga de destruir la flecha.
     */
    void DestroyProyectile(){
        Destroy(gameObject);
    }
}
