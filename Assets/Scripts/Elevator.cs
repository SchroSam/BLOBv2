using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class Elevator : MonoBehaviour
{
    public Vector3 targetTransform;
    private Vector3 previousTransform;
    private bool playerTrigger = false;
    private Vector3 velocity = Vector3.zero;
    public float speed = 0.3f;
    public float waitTime = 2.0f;
    public bool isLocked = false;
    private float fixedX;

    public void Start()
    {
        fixedX = gameObject.transform.position.x;
        previousTransform = transform.position;
        targetTransform.x = fixedX;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.gameObject.tag == "Player" && !playerTrigger)
            {
                if (isLocked == false)
                    StartCoroutine(PauseThenContinue());
            }
    }

    public void FixedUpdate()
    {
        if (playerTrigger)
        {
            if (isLocked == false)
            {
                Debug.Log("moving");
                if (targetTransform.y < transform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);

                    if (transform.position.y <= targetTransform.y)
                    {

                        playerTrigger = false;
                        targetTransform = previousTransform;
                        previousTransform = transform.position;
                        Debug.Log("new targetTransform y: " + targetTransform.y);
                    }
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);

                    if (transform.position.y >= targetTransform.y)
                    {

                        playerTrigger = false;
                        targetTransform = previousTransform;
                        previousTransform = transform.position;
                        Debug.Log("new targetTransform y: " + targetTransform.y);
                    }
            }
            }

            if (isLocked == true)
            {
                playerTrigger = false;
            }
                    
            }
            if (transform.position == targetTransform)
            {

                playerTrigger = false;
                targetTransform = previousTransform;
                previousTransform = transform.position;
                Debug.Log("new targetTransform y: " + targetTransform.y);
            }
    }
    public void lockVader(bool locker)
    {
        isLocked = locker;
    }

    IEnumerator PauseThenContinue()
    {
            yield return new WaitForSeconds(waitTime); // Wait for 2 seconds
            //gameObject.GetComponent<AudioSource>().Play();
            playerTrigger = true;
            Debug.Log("playerTrigger set True");
    }



}

