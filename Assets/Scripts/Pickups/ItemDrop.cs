using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject drop;
    private void OnDestroy()
    {
        Instantiate(drop, transform.position, transform.rotation);
    }
}
