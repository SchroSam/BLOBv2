using UnityEngine;

public class StartBlob : MonoBehaviour
{

    public Grow grower;
    public GameObject blob;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn(15);
    }

    public void Spawn(float time)
    {
        Instantiate(blob, transform.position, Quaternion.identity);
    }
}
