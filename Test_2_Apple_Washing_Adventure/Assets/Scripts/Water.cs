using UnityEngine;

public class Water : MonoBehaviour
{
    private AppleManager appleManager;

    [SerializeField] private float dirtTimer = 10f;

    private Renderer waterRend;

    [SerializeField] private Color startColour;
    [SerializeField] private Color dirtColour;

    public bool IsWaterClean = true;

    void Start()
    {
        appleManager = GameObject.Find("AppleManager").GetComponent<AppleManager>();
        waterRend = GetComponent<Renderer>();
        startColour = waterRend.material.color;

        Invoke("WaterGetsDirty", dirtTimer); // get water dirty
    }

    private void WaterGetsDirty()
    {
        waterRend.material.color = dirtColour;
        IsWaterClean = false;
    }

    private void OnMouseDown() // Check all apples in water
    {
        Invoke("WaterGetsDirty", dirtTimer);

        waterRend.material.color = startColour;
        IsWaterClean = true;

        foreach (GameObject apple in appleManager.AppleList)
        {
            apple.GetComponent<Apple>().WaterCleaned();
        }
    }
}
