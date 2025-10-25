using UnityEngine;
using System.Collections;


public class AnimQueue : MonoBehaviour
{

    public GameObject next;

    // Update is called once per frame
    public void GotDone(float delay)
    {


        if (next != null)
        {
            StartCoroutine(DeathSequence(delay));
        }
    }

    IEnumerator DeathSequence(float delay)
    {
        yield return new WaitForSeconds(delay);
        next.GetComponent<Animator>().SetTrigger("PreviousDone");
    }
}
