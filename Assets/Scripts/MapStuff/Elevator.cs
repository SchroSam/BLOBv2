using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.UIElements;

public class Elevator : MonoBehaviour
{
    public Transform targetTransform;
    private Vector3 previousPosition;
    private bool playerTrigger = false;
    public float speed = 0.3f;
    public float waitTime = 2.0f;
    public bool isLocked = false;
    private float fixedX;

    public void Start()
    {
        //fixedX = gameObject.transform.position.x;
        previousPosition = transform.position;
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
                if (targetTransform.position.y < transform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);

                    if (transform.position.y <= targetTransform.position.y)
                    {

                        playerTrigger = false;
                        targetTransform.position = previousPosition;
                        previousPosition = transform.position;
                        Debug.Log("new targetTransform y: " + targetTransform.position.y);
                    }
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);

                    if (transform.position.y >= targetTransform.position.y)
                    {

                        playerTrigger = false;
                        targetTransform.position = previousPosition;
                        previousPosition = transform.position;
                        Debug.Log("new targetTransform y: " + targetTransform.position.y);
                    }
            }
            }

            if (isLocked == true)
            {
                playerTrigger = false;
            }
                    
            }
            if (transform.position == targetTransform.position)
            {

                playerTrigger = false;
                targetTransform.position = previousPosition;
                previousPosition = transform.position;
                Debug.Log("new targetTransform y: " + targetTransform.position.y);
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

