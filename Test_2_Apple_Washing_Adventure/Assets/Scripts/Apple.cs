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

    private Renderer appleRend;
    private Rigidbody appleRB;

    [SerializeField] private int washTimer = 2;
    [SerializeField] private int overripeTime = 10;
    [SerializeField] private bool isInWater = false;


    void Start()
    {
        appleManager = GameObject.Find("AppleManager").GetComponent<AppleManager>();
        waterScript = GameObject.Find("Water").GetComponent<Water>();
        soundManager = GameObject.Find("Main Camera").GetComponent<SoundManager>();

        appleRend = gameObject.GetComponent<Renderer>();
        appleRB = gameObject.GetComponent<Rigidbody>();

        GetRandomColour();

        Invoke("OverripeApple", overripeTime);
    }

    private void GetRandomColour()
    {
        if (appleRend != null)
        {
            // Change the colour randomly
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            appleRend.material.color = randomColor;
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

        while (Time.time < endTime && isInWater) //while time isn't over
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

        // If time is over and still in water and its clean
        soundManager.PlayWashedScoredSound();

        GameObject effect = Instantiate(CleanEffectPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(effect, 2f);

        IsCleaned = true;
        appleRend.material.color = Color.red;
        appleManager.AppleScore++;
    }

    private void AppleInBasket()
    {
        soundManager.PlayCollectedScoreSound();

        IsInBasked = true;

        Debug.Log("Apple in Basket");

        appleManager.AppleScore++;

        appleRB.useGravity = false;
        appleRB.isKinematic = true;

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
