using UnityEngine;

public class Cam : MonoBehaviour
{
    public GameObject target;
    private float yspot;
    public int type;

    // Update is called once per frame
    private void Start()
    {
        if (type == 0)
        {
            yspot = transform.position.y;
        }
    }
    void Update()
    {
        if (type == 0)
        {
            transform.position = new Vector3(target.transform.position.x, yspot, -10);
        }
        else
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        }
    }
}
