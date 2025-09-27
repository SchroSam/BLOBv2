using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ArmEnemyMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    public int mode;
    [SerializeField] public Vector3 rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            Vector2 position = transform.position;
            position.x = position.x + (speed / 200);
            transform.position = position;
        }
        else
        {
            transform.Rotate(rotateSpeed);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mode = 1;
        }
    }
}
