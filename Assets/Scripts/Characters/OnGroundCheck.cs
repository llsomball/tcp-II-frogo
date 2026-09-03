using UnityEngine;

public class OnGroundCheck : MonoBehaviour
{
    public bool onGround;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Entrou");
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Saiu");
            onGround = false;
        }
    }
}
