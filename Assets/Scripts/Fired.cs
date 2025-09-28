using UnityEngine;

public class Fired : MonoBehaviour
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
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            right = GetComponent<Rigidbody2D>();
            Vector3 dir = new Vector3(100f, 0f, 0f);
            dir.Normalize();
            right.AddForce(-1 * ((dir * 100) * force));
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlobPhys") || other.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), other.GetComponent<CircleCollider2D>());
        }
        else if (other.CompareTag("Enemy") && other.GetComponent<LegEnemyMove>() != null)
        {
            other.GetComponent<LegEnemyMove>().hurt();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Puzz"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
        
    }
}
