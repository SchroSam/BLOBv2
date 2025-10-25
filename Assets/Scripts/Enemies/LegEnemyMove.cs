using System;
using System.Collections;
using UnityEngine;



public class LegEnemyMove : MonoBehaviour
{
    public float speed;
    public enum state {moving, attacking, stunned}
    public state mode = state.moving;
    public GameObject player;
    public Animator animator;
    private float tim;
    private int fCheck;
    private int pd = 0;
    private int pdt = 0;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        // foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects())
        // {
        //     TraverseHierarchy(root.transform);

        // }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("BlobPhys"))
        {
            Debug.Log("Ignoring collider of GameObject: " + collision.name);
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<CircleCollider2D>());

            for(int i = 1; i < collision.transform.childCount; i++)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.transform.GetChild(i).GetComponent<CircleCollider2D>());
            }
        }
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
            if (Vector2.Distance(transform.position, new Vector3(player.transform.position.x + player.transform.localScale.x / 2, player.transform.position.y, player.transform.position.z)) < 1f ||
               Vector2.Distance(transform.position, new Vector3(player.transform.position.x - player.transform.localScale.x / 2, player.transform.position.y, player.transform.position.z)) < 1f)
            {
                gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().linearVelocity.y);
                mode = state.attacking;
            }
            else if (player.transform.position.x > transform.position.x)
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

                if (gameObject.GetComponent<Rigidbody2D>().linearVelocity.x > -speed)
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
                if (tim > 2 && Math.Abs(player.transform.position.x - transform.position.x) > 1f)
                {
                    mode = state.moving;
                    tim = 0;
                    fCheck = 0;
                }
                if (tim >= 1 && fCheck != 1)
                {
                    //GameObject newObject = Instantiate(attack, transform.position, Quaternion.identity);
                    //StartCoroutine(PauseThenContinue());
                    fCheck = 1;
                }
            }
        }
        else if (mode == state.stunned)
        {
            tim -= Time.deltaTime;
            if (tim < 0)
            {
                mode = state.moving;
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

    IEnumerator PauseThenContinue()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f); // Wait for 2 seconds
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void spawnAttack()
    {
        StartCoroutine(PauseThenContinue());
    }
}
