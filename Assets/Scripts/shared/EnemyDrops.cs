using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public List<GameObject> possibleDrops; // Lista de posibles drops
    private string lastDrop = "None";

    private Dictionary<string, float[]> dropChances = new Dictionary<string, float[]>
    {
        {"None", new float[] {0.4f, 0.3f, 0.2f, 0.1f}},
        {"Green Flask", new float[] {0.3f, 0.4f, 0.2f, 0.1f}},
        {"Red Flask", new float[] {0.2f, 0.3f, 0.4f, 0.1f}},
        {"Blue Flask", new float[] {0.4f, 0.2f, 0.3f, 0.1f}}
    };

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
            Instantiate(possibleDrops[dropIndex], transform.position, Quaternion.identity);
            lastDrop = possibleDrops[dropIndex].name;
        }
    }
}
