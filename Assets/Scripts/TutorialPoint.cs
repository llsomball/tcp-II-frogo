using UnityEngine;
using System.Collections;

public class TutorialPoint : MonoBehaviour
{
    public GameObject tutorial1;
    public GameObject tutorial2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeTutorial());
        }
    }

    private IEnumerator ChangeTutorial()
    {
        tutorial1.SetActive(false);

        gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        tutorial2.SetActive(true);
    }
}