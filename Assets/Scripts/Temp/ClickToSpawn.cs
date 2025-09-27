using UnityEngine;
using System.Collections;
using System;

public class ClickToSpawn : MonoBehaviour {
    public GameObject prefab;
    public float requiredEmptyRadius;

    const int Left = 0;

    void Start()
    {
        prefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void Update() {
        if (Input.GetMouseButtonDown(Left)) {
            Vector2 position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Physics2D.OverlapCircle(position, requiredEmptyRadius)) {
                 prefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                Instantiate(prefab, position, Quaternion.identity);
            }
        }
    }
}
