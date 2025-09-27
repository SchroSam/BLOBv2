using UnityEngine;

public class IgnoreEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arm"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.GetComponent<BoxCollider2D>());
        }
        if (collision.CompareTag("Leg"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.GetComponent<BoxCollider2D>());
        }
        if (collision.CompareTag("Battery"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.GetComponent<BoxCollider2D>());
        }
    }
}
