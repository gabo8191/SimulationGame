using UnityEngine;

public static class RandomGenerator
{
    // Parámetros para el método de congruencia lineal
    private const float a = 1664525f;
    private const float c = 1013904223f;
    public const float m = 4294967296f;

    // Método estático para generar números pseudoaleatorios
    public static float Generate(float seed)
    {
        return (a * seed + c) % m;
    }

    // Método para obtener una semilla inicial válida
    public static float GetInitialSeed()
    {
        return Random.Range(0f, m);
    }
}
