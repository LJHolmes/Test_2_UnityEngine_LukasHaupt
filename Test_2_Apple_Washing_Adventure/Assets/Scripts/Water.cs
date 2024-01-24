using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float dirtTimer = 10f;

    private Renderer rend;

    [SerializeField] private Color startColour;
    [SerializeField] private Color dirtColour;

    public bool IsWaterClean = true;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;

        InvokeRepeating("WaterGetsDirty", dirtTimer, dirtTimer);
    }

    private void WaterGetsDirty()
    {
        rend.material.color = dirtColour;
        IsWaterClean = false;
    }

    private void OnMouseDown()
    {
        rend.material.color = startColour;
        IsWaterClean = true;
    }
}
