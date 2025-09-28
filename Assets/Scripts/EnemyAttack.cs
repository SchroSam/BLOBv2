using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float force = 8.0f;
    private Rigidbody2D right;
    public int z;
    void Start()
    {
        if (z > 0)
        {
            right = GetComponent<Rigidbody2D>();
            Vector3 dir = new Vector3(100f, 0f, 0f);
            dir.Normalize();
            right.AddForce((dir * 100) * force);
        }
        else
        {
            right = GetComponent<Rigidbody2D>();
            Vector3 dir = new Vector3(100f, 0f, 0f);
            dir.Normalize();
            right.AddForce(-1 * ((dir * 100) * force));
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Lattack"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.GetComponent<BoxCollider2D>());
        }
        else
        {
            Destroy(gameObject);
        }
    }
}