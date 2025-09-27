using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100.0f;
    private Rigidbody2D rigidbody2D;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Debug.Log("x: " + h + " y:" + v);

        Vector2 dir = new Vector2(h, v).normalized;
        rigidbody2D.AddForce(dir * speed);
    }
}