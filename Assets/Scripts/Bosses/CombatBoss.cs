using UnityEngine;

/*
 * Este script controla el comportamiento del jefe.
 */

public class CombatBoss : MonoBehaviour
{
    /*
     * Este método se llama cuando el jugador colisiona con el jefe final.
     */
    private float seed;
  
    /*
     *Genera un número aleatorio y lo utiliza para calcular el daño que se le hará al jugador.
     */
    private void Start()
    {
        seed = RandomGenerator.GetInitialSeed();
    }

    /*
     *Este método se llama cuando el jugador colisiona con el jefe final.
     *Si el jugador colisiona con el jefe final, se le resta vida al jugador.
     */

    public void ExecuteAttack()
    {
        float damage = CalculateMonteCarloDamage();
        GameManager.Instance.RestarVida((int)damage);
    }

    /*
     *Genera un número aleatorio y lo utiliza para calcular el daño que se le hará al jugador.
     */
    
    private float CalculateMonteCarloDamage()
    {
        seed = RandomGenerator.Generate(seed);  
        float rand = seed / 4294967296f;  

        if (rand < 0.6)  
        {
            return Random.Range(1, 2);  
        }
        else if (rand < 0.9)  
        {
            return Random.Range(2, 3);  
        }
        else 
        {
            return Random.Range(3, 5);  
        }
    }
}
