﻿using UnityEngine;
using System.Collections;
using System;

public class ClickToSpawn : MonoBehaviour {
    public GameObject blob;
    public float requiredEmptyRadius;

    const int Left = 0;

    void Start()
    {
        //blob.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void Update() {
        if (Input.GetMouseButtonDown(Left)) {
            Vector2 position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Physics2D.OverlapCircle(position, requiredEmptyRadius))
            {
                //blob.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                Instantiate(blob, position, Quaternion.identity);
                //GameObject newBlob = GameObject.Find("Blob(Clone)");
                //Debug.Log(newBlob.name);
                //newBlob.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
}
