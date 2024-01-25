using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;

    private Rigidbody rb;

    private Apple appleScript;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        appleScript = gameObject.GetComponent<Apple>();
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        if (appleScript != null)
        {
            if (appleScript.IsInBasked)
            {
                return;
            }

            appleScript.isGrabbedOnce = true;
        }

        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        if (appleScript != null)
        {
            if (appleScript.IsInBasked)
            {
                return;
            }
        }

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void OnMouseUp()
    {
        if (appleScript != null)
        {
            if (appleScript.IsInBasked)
            {
                return;
            }
            if (appleScript.IsCleaned)
            {
                rb.useGravity = true;
            }
        }
    }
}
