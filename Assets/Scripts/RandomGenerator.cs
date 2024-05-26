using UnityEngine;

/*
 *Este script genera n�meros aleatorios.
 */
public static class RandomGenerator
{
    /*
     *a: constante multiplicativa.
     *c: constante aditiva.
     *m: m�dulo.
     */
    private const float a = 1664525f;
    private const float c = 1013904223f;
    public const float m = 4294967296f;

    /*
     *Este m�todo genera un n�mero aleatorio.
     *seed: semilla para generar el n�mero aleatorio.
     */
    public static float Generate(float seed)
    {
        return (a * seed + c) % m;
    }

    /*
     *Este m�todo obtiene la semilla inicial.
     */

    public static float GetInitialSeed()
    {
        return Random.Range(0f, m);
    }
}
