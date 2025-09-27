using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LegEnemyMove : MonoBehaviour
{
    public float speed;
    public int mode = 0;
    public GameObject player;
    public GameObject attack;
    public Animator animator;
    private float tim;
    private int fCheck;
    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < )
    }


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
            }
            else
            {
                Vector2 position = transform.position;
                position.x = position.x - (speed / 200);
                transform.position = position;
            }
        }
        else if (mode == 1)
        {
            {

                tim += Time.deltaTime;
                if (tim > 2)
                {
                    if (player.transform.position.x == transform.position.x)
                    {
                        mode = 1;
                    }
                    else
                    {
                        mode = 0;
                    }
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
    }
    public void modeChange()
        {
            mode = 1;
        }
    }
