using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.UIElements;

public class Elevator : MonoBehaviour
{
    public Transform targetTransform;
    private Vector3 originalPosition;
    public bool playerTrigger = false;
    public bool returning = false;
    public bool returnStarted = false;
    public float speed = 0.3f;
    private float velocity;
    public float playerSenseDelay = 2.0f;
    public float returnDelay = 3.0f;

    public void Start()
    {
        originalPosition = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.gameObject.tag == "Player" && !playerTrigger && !returning)
            {
                StartCoroutine(PauseThenContinue(playerSenseDelay));
            }
    }

    public void FixedUpdate()
    {
        if (playerTrigger)
        {

            
            if (targetTransform.position != transform.position)
            {

                if (targetTransform.position.y < transform.position.y)
                    velocity = -speed;
                else
                    velocity = speed;

                transform.position = new Vector3(transform.position.x, transform.position.y + velocity, transform.position.z);

                if ((transform.position.y <= targetTransform.position.y && velocity < 0) || (transform.position.y >= targetTransform.position.y && velocity > 0))
                {

                    playerTrigger = false;
                    returning = true;
                    Debug.Log("returning");
                    
                    Debug.Log("arrived at target: " + targetTransform.position.y);
                }
            }

        }

        else if (returning && returnStarted)
        {
            
            if (originalPosition != transform.position)
            {

                if (originalPosition.y < transform.position.y)
                    velocity = -speed;
                else
                    velocity = speed;

                transform.position = new Vector3(transform.position.x, transform.position.y + velocity, transform.position.z);

                if ((transform.position.y <= originalPosition.y && velocity < 0) || (transform.position.y >= originalPosition.y && velocity > 0))
                {
                    
                    returning = false;
                    returnStarted = false;
                    
                    Debug.Log("arrived back home: " + originalPosition.y);
                }
            }
        }

        else if (returning)
        {
            StartCoroutine(PauseThenContinue(returnDelay));
        }

        

    }

    IEnumerator PauseThenContinue(float _waitTime)
    {
            yield return new WaitForSeconds(_waitTime);
        //gameObject.GetComponent<AudioSource>().Play();

        if (!returning)
        {
            playerTrigger = true;
            Debug.Log("playerTrigger set True");
        }
        else
        {
            returnStarted = true;
            Debug.Log("returnStarted set True");
        }
            

            
    }



}

