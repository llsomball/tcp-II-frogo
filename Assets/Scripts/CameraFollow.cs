using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float mouseSensitivity = 4f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -30f, 60f);

        transform.position = player.position + Quaternion.Euler(rotationX, rotationY, 0f) * new Vector3(0f, 3f, -5f);

        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}