using System;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public GameObject target;
    public int type;
    public float smoothTime = 0.3f;    // Damping time
    private Vector3 velocity = Vector3.zero;
    Vector3 targetTransform;

    // Update is called once per frame
    private void Start()
    {


    }
    void Update()
    {
        //if (Math.Abs(target.GetComponent<Rigidbody2D>().linearVelocity.y) > minYspeed)
        //{
        targetTransform = target.transform.position;
        targetTransform.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetTransform, ref velocity, smoothTime);


    }
        // else
        // {
        //     transform.position = new Vector3(target.transform.position.x, yspot, -10);
        // }
        // }
}
