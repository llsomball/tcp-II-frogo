using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0f, 180f ,0f);
    }
}