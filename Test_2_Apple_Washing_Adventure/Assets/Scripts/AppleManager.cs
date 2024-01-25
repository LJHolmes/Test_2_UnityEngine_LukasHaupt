using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleManager: MonoBehaviour
{
    public int AppleScore = 0;
    [SerializeField] private int appleMaxScore = 10;

    private GameObject winScreenPanel;

    [SerializeField] private List<GameObject> appleList;
    [SerializeField] private List<GameObject> appleInBaskedList;

    [SerializeField] private GameObject applePrefab;

    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private float spawnRange = 4f;

    private TMP_Text appleScoreText;
    private TMP_Text appleScoreMaxText;


    private void Start()
    {
        FindApples();

        winScreenPanel = GameObject.Find("WinScreen").transform.GetChild(0).gameObject;

        appleScoreText = GameObject.Find("ScoreValueText").GetComponent<TMP_Text>();
        appleScoreMaxText = GameObject.Find("ScoreMaxText").GetComponent<TMP_Text>();

        appleScoreMaxText.text = appleMaxScore.ToString();

        InvokeRepeating("SpawnAppleRandomInLocation", spawnTime, spawnTime);
    }

    private void Update()
    {
        if (AppleScore >= appleMaxScore)
        {
            winScreenPanel.SetActive(true);
        }

        appleScoreText.text = AppleScore.ToString();
    }

    private void SpawnAppleRandomInLocation()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-spawnRange, spawnRange), 3.5f, Random.Range(-spawnRange, spawnRange));

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

    public void AddToBasketList(GameObject apple)
    {
        appleInBaskedList.Add(apple);
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
