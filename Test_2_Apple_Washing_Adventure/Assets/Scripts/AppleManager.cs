using System.Collections.Generic;
using UnityEngine;

public class AppleManager: MonoBehaviour
{
    public int AppleScore = 0;

    [SerializeField] private List<GameObject> appleList;
    [SerializeField] private GameObject applePrefab;

    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private float spawnRange = 5f;


    private void Start()
    {
        FindApples();

        InvokeRepeating("SpawnAppleRandomInLocation", 0, spawnTime);
    }

    private void SpawnAppleRandomInLocation()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-spawnRange, spawnRange), 2.5f, Random.Range(-spawnRange, spawnRange));

        Vector3 spawnPosition = spawnLocation.transform.position + randomOffset;

        GameObject apple = Instantiate(applePrefab, spawnPosition, spawnLocation.transform.rotation);

        AddToList(apple);
    }

    private void FindApples()
    {
        foreach (GameObject apple in GameObject.FindGameObjectsWithTag("Apple"))
        {
            AddToList(apple);
        }
    }

    private void AddToList(GameObject apple)
    {
        appleList.Add(apple);
    }

    public void RemoveFromList(GameObject apple)
    {
        appleList.Remove(apple);
    }
}
