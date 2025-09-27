using UnityEngine;

public class PropogateCollisions : MonoBehaviour
{   void OnCollisionEnter2D(Collision2D collision) {
        transform.parent.SendMessage("OnCollisionEnter2D", collision);
    } 
}
