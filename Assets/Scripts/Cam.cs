using UnityEngine;

public class Cam : MonoBehaviour
{
    public GameObject target;
    private float yspot;

    // Update is called once per frame
    private void Start()
    {
        yspot = transform.position.y;
    }
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, yspot, -10);
    }
}
