using System;
using UnityEngine;



public class LegEnemyMove : MonoBehaviour
{
    public float speed;
    public enum state {moving, attacking, stunned}
    public state mode = state.moving;
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
    void FixedUpdate()
    {
        animator.SetInteger("mode", (int)mode);

        

        if (mode == state.moving)
        {
            if (player.transform.position.x > transform.position.x)
            {
                // Vector2 position = transform.position;
                // position.x = position.x + (speed / 200);
                // transform.position = position;

                if (gameObject.GetComponent<Rigidbody2D>().linearVelocity.x < speed)
                    gameObject.GetComponent<Rigidbody2D>().AddForceX(speed);
                    

                pd = 1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                // Vector2 position = transform.position;
                // position.x = position.x - (speed / 200);
                // transform.position = position;

                if(gameObject.GetComponent<Rigidbody2D>().linearVelocity.x > -speed)
                    gameObject.GetComponent<Rigidbody2D>().AddForceX(-speed);

                pd = 0;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            tim += Time.deltaTime;
            if (pd != pdt)
            {
                mode = state.stunned;
                tim = 2 + UnityEngine.Random.Range(0, 0.5f);
                pdt = pd;
            }
        }
        else if (mode == state.attacking)
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
        else if (mode == state.stunned)
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
            mode = state.attacking;
            tim = 0;
        }
    public void hurt()
    {
        health -= 1;
        if (health == 0)
        {
            //Debug.Log("I, " + gameObject.name + " just killed myself");
            Destroy(gameObject);
        }
    }
}
