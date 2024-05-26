using UnityEngine;

/*
 * Este script controla el comportamiento del jefe.
 */

public class CombatBoss : MonoBehaviour
{
    /*
     * Este m�todo se llama cuando el jugador colisiona con el jefe final.
     */
    private float seed;
  
    /*
     *Genera un n�mero aleatorio y lo utiliza para calcular el da�o que se le har� al jugador.
     */
    private void Start()
    {
        seed = RandomGenerator.GetInitialSeed();
    }

    /*
     *Este m�todo se llama cuando el jugador colisiona con el jefe final.
     *Si el jugador colisiona con el jefe final, se le resta vida al jugador.
     */

    public void ExecuteAttack()
    {
        float damage = CalculateMonteCarloDamage();
        GameManager.Instance.RestarVida((int)damage);
    }

    /*
     *Genera un n�mero aleatorio y lo utiliza para calcular el da�o que se le har� al jugador.
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
