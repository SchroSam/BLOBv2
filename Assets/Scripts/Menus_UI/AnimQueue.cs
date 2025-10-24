using UnityEngine;

public class AnimQueue : MonoBehaviour
{

    public GameObject next;

    // Update is called once per frame
    public void GotDone()
    {
        if(next != null)
        {
            next.GetComponent<Animator>().SetTrigger("PreviousDone");
        }
    }
}
