using UnityEngine;

public class Apple : MonoBehaviour
{
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

    private void AppleInBasket()
    {
        soundManager.PlayAppleWashSound();
        IsInBasked = true;
        Debug.Log("Apple in Basket");
        appleManager.AppleScore++;
        rb.useGravity = false;
        rb.isKinematic = true;

        appleManager.RemoveFromList(gameObject);
        appleManager.AddToBasketList(gameObject);
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

        soundManager.PlayAppleWashSound();
        IsCleaned = true;
        rend.material.color = Color.red;
        appleManager.AppleScore++;
    }

    private void OverripeApple()
    {
        if (IsCleaned || isGrabbedOnce)
        {
            return;
        }

        rb.useGravity = true;

        Invoke("SelfDestruction", washTimer);
    }

    private void SelfDestruction()
    {
        appleManager.RemoveFromList(gameObject);

        Destroy(gameObject);
    }
}
