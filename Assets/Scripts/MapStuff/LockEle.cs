using UnityEngine;

public class LockEle : MonoBehaviour
{
    public GameObject lockTarget;
    public bool lockingIt = false;
    // Update is called once per frame
    private void OnDestroy()
    {
        if (lockTarget.GetComponent<Elevator>() != null && gameObject != null)
        {
            Debug.Log(lockingIt.ToString());
            lockTarget.GetComponent<Elevator>().enabled = lockingIt;
        }
    }
}
