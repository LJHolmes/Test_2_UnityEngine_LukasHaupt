using System.Collections.Generic;
using UnityEngine;

public class AppleManager
    : MonoBehaviour
{
    public List<GameObject> AppleList;
    public GameObject ApplePrefab;

    public float SpawnTime = 1;
    public GameObject SpawnLocation;

    private void Start()
    {
        FindApples();

        InvokeRepeating("SpawnApple", 0, SpawnTime);
    }

    private void SpawnApple()
    {
        GameObject apple = Instantiate(ApplePrefab, SpawnLocation.transform.position, Quaternion.identity);

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
        AppleList.Add(apple);
    }

    public void RemoveFromList(GameObject apple)
    {
        AppleList.Remove(apple);
    }

    public void Loop()
    {
        for (int i = 0; i < AppleList.Count; i++)
        {
            for (i = 0; i < AppleList.Count; i++)
            {
                GameObject apple = AppleList[i];
                Renderer renderer = apple.GetComponent<Renderer>();

                if (renderer != null)
                {
                    // Change the color randomly
                    Color randomColor = new Color(Random.value, Random.value, Random.value);
                    renderer.material.color = randomColor;
                }
                else
                {
                    return;
                }
            }
        }

        foreach (GameObject apple in AppleList)
        {
            Vector3 pos = apple.transform.position;
            Vector3 newPos = new Vector3(0, 2, 0);

            apple.transform.position = pos + newPos;
        }
    }
}
