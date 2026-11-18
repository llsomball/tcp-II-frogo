using UnityEngine;

public class BossShockwave : MonoBehaviour
{
    [SerializeField] private float expansionSpeed = 5f;
    [SerializeField] private float maxSize = 8f;

    private void Update()
    {
        transform.localScale += Vector3.one * expansionSpeed * Time.deltaTime;

        if (transform.localScale.x >= maxSize)
        {
            Destroy(gameObject);
        }
    }
}
