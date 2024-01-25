using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;

    private Rigidbody appleRB;

    private Apple appleScript;

    private void Start()
    {
        appleRB = gameObject.GetComponent<Rigidbody>();
        appleScript = gameObject.GetComponent<Apple>();
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown() // take object, get mouse Pos
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

    private void OnMouseDrag() // take object with mouse
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

    private void OnMouseUp() // Drop object
    {
        if (appleScript != null)
        {
            if (appleScript.IsInBasked)
            {
                return;
            }
            if (appleScript.IsCleaned)
            {
                appleRB.useGravity = true;
            }
        }
    }
}
