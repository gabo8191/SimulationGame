using UnityEngine;

public class CombatBoss : MonoBehaviour
{
    private float seed;
  


    private void Start()
    {
        seed = RandomGenerator.GetInitialSeed();
    }

    public void ExecuteAttack()
    {
        float damage = CalculateMonteCarloDamage();
        GameManager.Instance.RestarVida((int)damage);
    }
    
    private float CalculateMonteCarloDamage()
    {
        seed = RandomGenerator.Generate(seed);  
        float rand = seed / 4294967296f;  

        if (rand < 0.6)  
        {
            return Random.Range(1, 3);  
        }
        else if (rand < 0.9)  
        {
            return Random.Range(3, 5);  
        }
        else 
        {
            return Random.Range(5, 7);  
        }
    }
}
