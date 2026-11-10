using UnityEngine;

public class BossBox : MonoBehaviour
{
    [SerializeField] private GameObject warningCircle;

    public void SetWarning(GameObject warning)
    {
        warningCircle = warning;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoxWarning"))
        {
            Destroy(warningCircle);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(warningCircle);
            Destroy(gameObject);
        }
    }
}
