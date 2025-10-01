using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LegEnemyMove : MonoBehaviour
{
    public float speed;
    public int mode = 0;
    public GameObject player;
    public GameObject attack;
    public Animator animator;
    private float tim;
    private int fCheck;
    private int pd = 0;
    private int pdt = 0;
    public int health;
    private SpriteRenderer spriteRenderer;

    public float interval = 0.5f;
    public float lastWalk = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects())
        // {
        //     TraverseHierarchy(root.transform);

        // }

    }

    // void FixedUpdate()
    // {
    //     while (animator.GetFloat("mode") == 0.0f)
    //     {
    //         if (Time.time > lastWalk + interval)
    //         {
    //             lastWalk = Time.time;
    //             gameObject.GetComponent<AudioSource>().Play();
    //         }
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("mode", mode);

        

        if (mode == 0)
        {
            if (player.transform.position.x > transform.position.x)
            {
                Vector2 position = transform.position;
                position.x = position.x + (speed / 200);
                transform.position = position;
                pd = 1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                Vector2 position = transform.position;
                position.x = position.x - (speed / 200);
                transform.position = position;
                pd = 0;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            tim += Time.deltaTime;
            if (pd != pdt)
            {
                mode = 2;
                tim = 2 + (Random.Range(0, 0.5f));
                pdt = pd;
            }
        }
        else if (mode == 1)
        {
            {

                tim += Time.deltaTime;
                if (tim > 2)
                {
                    mode = 0;
                    tim = 0;
                    fCheck = 0;
                }
                if (tim >= 1 && fCheck != 1)
                {
                    GameObject newObject = Instantiate(attack, transform.position, Quaternion.identity);
                    fCheck = 1;
                    if (player.transform.position.x > transform.position.x)
                    {
                        newObject.GetComponent<EnemyAttack>().z = 1;
                    }
                    else
                    {
                        newObject.GetComponent<EnemyAttack>().z = 0;
                    }
                }
            }
        }
        else
        {
            tim -= Time.deltaTime;
            if (tim < 0)
            {
                mode = 0;
                tim = 0;
            }
        }
    }
    public void modeChange()
        {
            mode = 1;
            tim = 0;
        }
    public void hurt()
    {
        health -= 1;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
