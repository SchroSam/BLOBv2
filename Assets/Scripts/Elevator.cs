using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    public Vector3 targetTransform;
    private Vector3 previousTransform;
    private bool playerTrigger = false;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;
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
        if (isLocked != true)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(PauseThenContinue());
            }
        }
    }

    public void Update()
    {
        if (isLocked != true)
        {
            if (playerTrigger)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetTransform, ref velocity, smoothTime);
            }
            if (transform.position == targetTransform)
            {
                playerTrigger = false;
                targetTransform = previousTransform;
                previousTransform = transform.position;
            }
        }
    }
    public void lockVader(bool locker)
    {
        isLocked = locker;
    }

    IEnumerator PauseThenContinue()
    {
        if (isLocked != true)
        {
            yield return new WaitForSeconds(waitTime); // Wait for 2 seconds
            playerTrigger = true;
            Debug.Log("playerTrigger set True");
            Debug.Log("is unlocked");
        }
    }



}

