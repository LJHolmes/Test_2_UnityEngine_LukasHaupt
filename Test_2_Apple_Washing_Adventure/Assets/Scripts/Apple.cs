using UnityEngine;

public class Apple : MonoBehaviour
{
    private AppleManager appleManager;
    private Renderer rend;
    private Rigidbody rb;

    private Water waterScript;

    [SerializeField] private int WashTimer = 1;
    [SerializeField] private bool isCleanable = false;
    [SerializeField] private bool isCleaned = false;
    [SerializeField] private bool isFinished = false;

    void Start()
    {
        appleManager = GameObject.Find("AppleManager").GetComponent<AppleManager>();
        rend = gameObject.GetComponent<Renderer>();
        waterScript = GameObject.Find("Water").GetComponent<Water>();

        GetRandomColour();
    }

    void Update()
    {
        if (isCleanable)
        {
            if (waterScript.IsWaterClean)
            {
                Invoke("CleanApple", WashTimer);
            }
        }
        if (isFinished)
        {
            rb.isKinematic = true;
        }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            isCleanable = true;
        }
        else
        {
            isCleanable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Basket")
        {
            if (isCleaned)
            {
                Debug.Log("Apple in Basket");

                // Sound abspielen

                appleManager.AppleScore++;

                isFinished = true;
            }
        }
    }

    private void CleanApple()
    {
        rend.material.color = Color.red;

        appleManager.AppleScore++;

        isCleaned = true;
    }
}
