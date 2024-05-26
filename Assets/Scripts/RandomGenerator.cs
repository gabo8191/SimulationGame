using UnityEngine;

/*
 *Este script genera números aleatorios.
 */
public static class RandomGenerator
{
    /*
     *a: constante multiplicativa.
     *c: constante aditiva.
     *m: módulo.
     */
    private const float a = 1664525f;
    private const float c = 1013904223f;
    public const float m = 4294967296f;

    /*
     *Este método genera un número aleatorio.
     *seed: semilla para generar el número aleatorio.
     */
    public static float Generate(float seed)
    {
        return (a * seed + c) % m;
    }

    /*
     *Este método obtiene la semilla inicial.
     */

    public static float GetInitialSeed()
    {
        return Random.Range(0f, m);
    }
}
