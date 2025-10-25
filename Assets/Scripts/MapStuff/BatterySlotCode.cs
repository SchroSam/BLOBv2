using System.Threading;
using TMPro;
using UnityEngine;

public class BatterySlotCode : MonoBehaviour
{
    public bool isActive = false;
    public GameObject visual;
    public GameObject doorLink;
    public Vector2 pos = new Vector2(0,0);
    private Vector2 origPos;

    // Update is called once per frame
    private void Start()
    {
        origPos = doorLink.transform.position;
    }
    void Update()
    {
        if (isActive)
        {
            visual.SetActive(true);
            doorLink.transform.position = Vector3.MoveTowards(doorLink.transform.position, origPos + pos, 2 * Time.deltaTime);
        }

        if (doorLink.transform.position.y >= origPos.y + (pos.y / 2))
        {
            doorLink.GetComponent<BoxCollider2D>().enabled = false;
        }


        if(doorLink.transform.position.y >= origPos.y + pos.y)
        {
            doorLink.SetActive(false);
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FBAT"))
        {
            gameObject.GetComponent<AudioSource>().Play();
            isActive = true;
            collision.gameObject.SetActive(false);
        }
    }
}
