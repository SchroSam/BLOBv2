using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class ArmEnemyMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    public int mode;
    [SerializeField] public Vector3 rotateSpeed;
    public float shrinkDuration = 2f; // Time to fully shrink
    public Vector3 targetScale = new Vector3(0, 0, 0); // Final size
    private Vector3 initialScale;
    private float elapsedTime = 0f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }


    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            if (player.transform.position.x > transform.position.x)
            {
                Vector2 position = transform.position;
                position.x = position.x + (speed / 200);
                transform.position = position;
            }
            else if (player.transform.position.x < transform.position.x)
            {
                Vector2 position = transform.position;
                position.x = position.x - (speed / 200);
                transform.position = position;
            }
        }
        else
        {
            transform.Rotate(rotateSpeed);
            if (elapsedTime < shrinkDuration)
            {
                elapsedTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / shrinkDuration);
                GetComponent<BoxCollider2D>().enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 8 * Time.deltaTime);
            }
            if (transform.localScale.y <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
        public void modeChange()
    {
        mode = 1;
    }
}

