using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleManager: MonoBehaviour
{
    public int AppleScore = 0;

    public TMP_Text AppleScoreText;
    public TMP_Text AppleScoreMaxText;

    [SerializeField] private int appleMaxScore = 10;

    private GameObject winScreenPanel;

    [SerializeField] private List<GameObject> appleList;
    [SerializeField] private GameObject applePrefab;

    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private float spawnRange = 5f;


    private void Start()
    {
        FindApples();

        winScreenPanel = GameObject.Find("WinScreen").transform.GetChild(0).gameObject;

        AppleScoreMaxText.text = appleMaxScore.ToString();

        InvokeRepeating("SpawnAppleRandomInLocation", 0, spawnTime);
    }

    private void Update()
    {
        if (AppleScore >= appleMaxScore)
        {
            winScreenPanel.SetActive(true);
        }

        AppleScoreText.text = AppleScore.ToString();
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

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
