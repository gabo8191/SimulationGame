using System.Collections.Generic;
using UnityEngine;

/*
 * * Este script controla el comportamiento de los objetos que dejan caer los enemigos.
 * */

public class EnemyDrops : MonoBehaviour
{
    /*
     * possibleDrops: Lista de objetos que pueden ser dropeados por el enemigo.
     * lastDrop: Nombre del último objeto dropeado.
     * 
     */
    public List<GameObject> possibleDrops;
    private string lastDrop = "EmptyPrefab";

    /*
     * dropChances: Diccionario que contiene las probabilidades de que un objeto sea dropeado. 
     */
    private Dictionary<string, float[]> dropChances = new Dictionary<string, float[]>
    {
        {"EmptyPrefab", new float[] {0.7f, 0.1f, 0.1f, 0.1f}},  
        {"Drop1", new float[] {0.6f, 0.2f, 0.1f, 0.1f}},  
        {"Drop2", new float[] {0.6f, 0.1f, 0.2f, 0.1f}},    
        {"Drop3", new float[] {0.6f, 0.1f, 0.1f, 0.2f}}    
    };

    /*
     * Este método se llama cuando el enemigo es destruido.
     * Se encarga de dropear un objeto.
     * Si el enemigo es destruido, se dropea un objeto de la lista de objetos posibles.
     * Invoca al método DropItem.
     */

    public void DropItem()
    {
        float rand = Random.value;
        float cumulative = 0f;
        int dropIndex = -1;

        for (int i = 0; i < possibleDrops.Count; i++)
        {
            cumulative += dropChances[lastDrop][i];
            if (rand < cumulative)
            {
                dropIndex = i;
                break;
            }
        }

        if (dropIndex != -1)
        {
            GameObject drop = Instantiate(possibleDrops[dropIndex], transform.position, Quaternion.identity);
            lastDrop = possibleDrops[dropIndex].name;

            if (lastDrop.Equals("Drop1"))
            {
                drop.AddComponent<DropLife>();
            }else if (lastDrop.Equals("Drop2"))
            {
                drop.AddComponent<DropJump>();
            }else if (lastDrop.Equals("Drop3"))
            {
                drop.AddComponent<DropSpeed>();
            }
        }
    }
}
