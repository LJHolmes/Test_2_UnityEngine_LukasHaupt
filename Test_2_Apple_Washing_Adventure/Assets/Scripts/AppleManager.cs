using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleManager: MonoBehaviour
{
    public List<GameObject> AppleList;

    public int AppleScore = 0;
    [SerializeField] private int appleMaxScore = 10;

    private GameObject winScreenPanel;
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
        //update texts

        if (AppleScore >= appleMaxScore)
        {
            winScreenPanel.SetActive(true);
        }

        appleScoreText.text = AppleScore.ToString();
    }

    private void SpawnAppleRandomInLocation()
    {
        // generate random offset
        Vector3 randomOffset = new Vector3(Random.Range(-spawnRange, spawnRange), 3.5f, Random.Range(-spawnRange, spawnRange));

        // add to the spawn pos
        Vector3 spawnPosition = spawnLocation.transform.position + randomOffset;

        GameObject apple = Instantiate(applePrefab, spawnPosition, spawnLocation.transform.rotation);

        AddToAppleList(apple);
    }

    private void FindApples()
    {
        foreach (GameObject apple in GameObject.FindGameObjectsWithTag("Apple"))
        {
            AddToAppleList(apple);
        }
    }

    private void AddToAppleList(GameObject apple)
    {
        AppleList.Add(apple);
    }

    public void AddToBasketList(GameObject apple)
    {
        appleInBaskedList.Add(apple);
    }

    public void RemoveFromAppleList(GameObject apple)
    {
        AppleList.Remove(apple);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
