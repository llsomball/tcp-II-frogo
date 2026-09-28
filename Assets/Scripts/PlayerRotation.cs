using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        Vector3 cameraDirection = cameraTransform.forward;
        cameraDirection.y = 0f;

        if (cameraDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameraDirection) * Quaternion.Euler(0f, 90f, 0f);
        }
    }
}