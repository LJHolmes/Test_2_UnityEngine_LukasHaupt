using UnityEngine;

public class OverripeApple : MonoBehaviour
{
    void Start()
    {
        Invoke("SelfDestruction", 3f);
    }

    private void SelfDestruction()
    {
        Destroy(gameObject);
    }
}
