using UnityEngine;

public class OnDestroyed : MonoBehaviour
{
    public GameObject drop;
    private void OnDestroy()
    {
        Instantiate(drop, transform.position, transform.rotation);
    }
}
