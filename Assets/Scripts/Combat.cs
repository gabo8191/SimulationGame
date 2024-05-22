using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    private float dañoGolpe; // Ahora se calculará en el método Golpe()
    private float seed;  // Semilla para el generador de números pseudoaleatorios

    private void Start()
    {
        seed = RandomGenerator.GetInitialSeed(); // Inicializar semilla para el generador
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Golpe();
        }
    }

    private void Golpe()
    {
        dañoGolpe = CalcularDañoMonteCarlo();
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.GetComponent<Health>().TakeDamage(dañoGolpe);
            }
        }
    }

    private float CalcularDañoMonteCarlo()
    {
        // Actualizar la semilla usando el generador de números pseudoaleatorios
        seed = RandomGenerator.Generate(seed);
        float rand = seed / 4294967296f; // Convertir semilla a un valor entre 0 y 1

        if (rand < 0.5)  // 50% de probabilidad
        {
            return Random.Range(10, 16);  // Daño entre 10 y 15
        }
        else if (rand < 0.8)  // 30% de probabilidad adicional
        {
            return Random.Range(16, 26);  // Daño entre 16 y 25
        }
        else  // 20% restante
        {
            return Random.Range(26, 36);  // Daño entre 26 y 35
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
