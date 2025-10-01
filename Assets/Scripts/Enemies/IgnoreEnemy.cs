using UnityEngine;
using UnityEngine.SceneManagement;

public class IgnoreEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Arm"))
    //     {
    //         Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.GetComponent<BoxCollider2D>());
    //     }
    //     if (collision.CompareTag("Leg"))
    //     {
    //         Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.GetComponent<BoxCollider2D>());
    //     }
    //     if (collision.CompareTag("Battery"))
    //     {
    //         Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.GetComponent<BoxCollider2D>());
    //     }
    // }

    void TraverseHierarchy(Transform current)
    {
        //Debug.Log("Found: " + current.name);
        if (current.gameObject.tag == "Enemy")
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), current.gameObject.GetComponent<BoxCollider2D>());
            }


        foreach (Transform child in current)
        {

            if (child.gameObject.tag == "Enemy")
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), child.gameObject.GetComponent<BoxCollider2D>());
            }
            TraverseHierarchy(child);
        }
    }
    private void Awake()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>(true);

        foreach (GameObject obj in allObjects)
        {
            TraverseHierarchy(obj.transform);
        }
    }
}
