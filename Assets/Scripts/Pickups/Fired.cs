using UnityEngine;

public class Fired : MonoBehaviour
{
    public float force = 8.0f;
    private Rigidbody2D right;
    public int z;
    void Start()
    {
            right = GetComponent<Rigidbody2D>();
            Vector2 dir = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().linearVelocity;
            dir.Normalize();
            dir.x += GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().linearVelocityX/10f;
            right.AddForce((dir * 100) * force);
            if(z <= 0)
                transform.localRotation = Quaternion.Euler(0, 180, 0);
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
        else if (other.CompareTag("Puzz2") && gameObject.CompareTag("FBAT"))
        {
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Puzz") && !gameObject.CompareTag("FBAT"))
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
