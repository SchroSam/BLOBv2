using UnityEngine;

public class EnemiesIgnore : MonoBehaviour
{
    // void TraverseHierarchy(Transform current)
    // {
    //     Debug.Log("Found: " + current.name);
    //     if (current.gameObject.tag == "Arm" || current.gameObject.tag == "Battery" || current.gameObject.tag == "Leg")
    //     {
    //        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), current.gameObject.GetComponent<BoxCollider2D>());
    //     }


    //     foreach (Transform child in current)
    //     {

    //         if (child.gameObject.tag == "Arm" || child.gameObject.tag == "Battery" || child.gameObject.tag == "Leg" || child.gameObject.tag == "Ground")
    //         {
    //             Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), child.gameObject.GetComponent<BoxCollider2D>());
    //         }
    //         TraverseHierarchy(child);
    //     }
    // }
    private void Awake()
    {
        //GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        TempEnemy[] pickups = FindObjectsByType<TempEnemy>(FindObjectsSortMode.None);
        Elevator[] eles = FindObjectsByType<Elevator>(FindObjectsSortMode.None);
        LegEnemyMove[] legEnemies = FindObjectsByType<LegEnemyMove>(FindObjectsSortMode.None);
        Bomb[] bombs = FindObjectsByType<Bomb>(FindObjectsSortMode.None);

        foreach (TempEnemy obj in pickups)
        {
            //TraverseHierarchy(obj.transform);
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.gameObject.GetComponent<BoxCollider2D>());
        }

        foreach (Elevator obj in eles)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.gameObject.GetComponent<BoxCollider2D>());
        }

        foreach (LegEnemyMove obj in legEnemies)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.gameObject.GetComponent<BoxCollider2D>());
        }

        foreach(Bomb obj in bombs)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), obj.gameObject.GetComponent<CircleCollider2D>());
        }

    }
}
