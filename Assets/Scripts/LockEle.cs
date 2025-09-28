using UnityEngine;

public class LockEle : MonoBehaviour
{
    public GameObject lockTarget;
    public bool lockingIt = false;
    // Update is called once per frame
    private void OnDestroy()
    {
        if (lockTarget.GetComponent<Elevator>() != null)
        {
            lockTarget.GetComponent<Elevator>().lockVader(lockingIt);
        }
    }
}
