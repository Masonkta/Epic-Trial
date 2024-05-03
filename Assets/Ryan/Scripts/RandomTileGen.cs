using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileGen : MonoBehaviour
{
    // List of prefabs to choose from
    public List<GameObject> prefabsList = new List<GameObject>();

    // Number of prefabs to instantiate
    public int numberOfPrefabsToInstantiate = 1;

    // Initial position to start spawning prefabs
    public Vector3 initialSpawnPosition = Vector3.zero;

    // Distance between each prefab in the X and Z direction
    public float xDistanceBetweenPrefabs = 10f;
    public float zDistanceBetweenPrefabs = 10f;


    void Start()
    {
        Vector3 spawnPosition = initialSpawnPosition;

        // Instantiate prefabs sequentially with the specified distance between them
        for (int i = 0; i < Mathf.Min(numberOfPrefabsToInstantiate, prefabsList.Count); i++)
        {
            GameObject prefab = prefabsList[Random.Range(0, prefabsList.Count)];
            
            // Instantiate the prefab at the calculated spawn position
            GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);

            // Update the spawn position for the next prefab
            spawnPosition.x += xDistanceBetweenPrefabs;
            spawnPosition.z += zDistanceBetweenPrefabs;
        }
    }
}
