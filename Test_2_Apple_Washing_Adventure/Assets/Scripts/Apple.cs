using UnityEngine;

public class Apple : MonoBehaviour
{
    public GameObject OverripeApplePrefab;
    public GameObject CleanEffectPrefab;

    public bool IsInBasked = false;
    public bool IsCleaned = false;
    public bool isGrabbedOnce = false;

    private AppleManager appleManager;
    private Water waterScript;
    private SoundManager soundManager;

    private Renderer rend;
    private Rigidbody rb;

    [SerializeField] private int washTimer = 2;
    [SerializeField] private int overripeTime = 10;
    [SerializeField] private bool isInWater = false;


    void Start()
    {
        appleManager = GameObject.Find("AppleManager").GetComponent<AppleManager>();
        waterScript = GameObject.Find("Water").GetComponent<Water>();
        soundManager = GameObject.Find("Main Camera").GetComponent<SoundManager>();

        rend = gameObject.GetComponent<Renderer>();
        rb = gameObject.GetComponent<Rigidbody>();

        GetRandomColour();

        Invoke("OverripeApple", overripeTime);
    }

    private void GetRandomColour()
    {
        if (rend != null)
        {
            // Change the color randomly
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            rend.material.color = randomColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water" && !IsCleaned)
        {
            isInWater = true;

            StartCoroutine(CleanApple());
        }

        if (other.gameObject.tag == "Basket" && IsCleaned && !IsInBasked)
        {
            AppleInBasket();
        }
    }

    System.Collections.IEnumerator CleanApple()
    {
        float endTime = Time.time + washTimer;

        while (Time.time < endTime && isInWater)
        {
            if (waterScript.IsWaterClean)
            {
                yield return null;
            }
            else
            {
                yield break;
            }
        }

        soundManager.PlayWashedScoredSound();

        GameObject effect = Instantiate(CleanEffectPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(effect, 2f);

        IsCleaned = true;
        rend.material.color = Color.red;
        appleManager.AppleScore++;
    }

    private void AppleInBasket()
    {
        soundManager.PlayCollectedScoreSound();

        IsInBasked = true;

        Debug.Log("Apple in Basket");

        appleManager.AppleScore++;

        rb.useGravity = false;
        rb.isKinematic = true;

        appleManager.RemoveFromAppleList(gameObject);
        appleManager.AddToBasketList(gameObject);
    }

    private void OverripeApple()
    {
        if (IsCleaned || isGrabbedOnce)
        {
            return;
        }

        appleManager.RemoveFromAppleList(gameObject);

        Instantiate(OverripeApplePrefab, gameObject.transform.position, gameObject.transform.rotation);

        Destroy(gameObject);
    }

    public void WaterCleaned()
    {
        if (!IsCleaned && isInWater)
        {
            StartCoroutine(CleanApple());
        }
    }
}
