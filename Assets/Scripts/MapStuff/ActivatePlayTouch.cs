using UnityEngine;

public class ActivatePlayTouch : MonoBehaviour
{
    public GameObject activate;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            activate.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
