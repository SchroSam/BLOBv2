using System.Threading;
using TMPro;
using UnityEngine;

public class BatterySlotCode : MonoBehaviour
{
    public bool isActive = false;
    public GameObject visual;
    public GameObject doorLink;
    public Vector2 pos = new Vector2(0,0);
    private Vector2 qaid;

    // Update is called once per frame
    private void Start()
    {
        qaid = doorLink.transform.position;
    }
    void Update()
    {
        if (isActive)
        {
            visual.SetActive(true);
            doorLink.transform.position = Vector3.MoveTowards(doorLink.transform.position, qaid + pos, 2 * Time.deltaTime);
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
