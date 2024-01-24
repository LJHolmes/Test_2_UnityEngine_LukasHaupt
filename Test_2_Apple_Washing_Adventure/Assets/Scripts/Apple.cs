using System.Collections;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public int WashTimer = 1;

    private AppleManager appleManager;

    void Start()
    {
        appleManager = GameObject.Find("AppleManager").GetComponent<AppleManager>();
    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {

        }
    }

    private IEnumerator Damaging()
    {
        yield return new WaitForSeconds(WashTimer);

        
    }
}
