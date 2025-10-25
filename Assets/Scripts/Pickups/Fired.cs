using UnityEngine;

public class Fired : MonoBehaviour
{
    public float knockBackMultiplier = 10.0f;
    public float knockBackResistance = 5.0f;
    public float maxVerticalForce = 0.5f;
    public float minHorizontalForce = 10f;
    private int damage = 1;
    private Rigidbody2D right;
    
    void Start()
    {
        right = GetComponent<Rigidbody2D>();
        Vector2 dir = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().linearVelocity;

        if (dir.x < 0 && dir.x > -0.5)
            dir.x = -dir.x;
        //dir.Normalize();
        dir.x += GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().linearVelocityX / 10f;

        
        if (dir.x < minHorizontalForce && dir.x >= 0)
            dir.x = minHorizontalForce;
        else if (dir.x > -minHorizontalForce && dir.x < 0)
            dir.x = -minHorizontalForce;

        if (dir.y > maxVerticalForce)
            dir.y = maxVerticalForce;
        else if (dir.y < -maxVerticalForce)
            dir.y = -maxVerticalForce;

        right.AddForce(dir * 100);
        if(dir.x <= 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlobPhys") || other.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), other.GetComponent<CircleCollider2D>());
        }
        else if ((other.CompareTag("Enemy") && other.GetComponent<LegEnemyMove>() != null) || other.CompareTag("Boss") && other.GetComponent<BossController>() != null)
        {
            Debug.Log(other.tag);
            Vector2 knockBackVector = gameObject.GetComponent<Rigidbody2D>().linearVelocity;
            Vector2 currentVelocity = other.gameObject.GetComponent<Rigidbody2D>().linearVelocity;

            // if (Math.Abs(gameObject.GetComponent<Rigidbody2D>().linearVelocity.x) < minimumKnockback)
            // {
            //     if (knockBackVector.x >= 0)
            //         knockBackVector.x = minimumKnockback;
            //     else
            //         knockBackVector.x = -minimumKnockback;
            // }

            // Debug.Log(knockBackVector);
            // Debug.Log(other.gameObject.GetComponent<Rigidbody2D>().linearVelocity);

            // if ((knockBackVector.x >= 0 && currentVelocity.x >= 0) || (knockBackVector.x < 0 && currentVelocity.x < 0))
            // {
            //     other.gameObject.GetComponent<Rigidbody2D>().linearVelocity = knockBackVector - currentVelocity;
            // }
            // else
            // {
            //     if(knockBackVector.x >= 0)
            //     {
            if (other.tag == "Enemy")
                other.gameObject.GetComponent<Rigidbody2D>().linearVelocity = currentVelocity + knockBackVector / knockBackResistance;
            else
                other.gameObject.GetComponent<Rigidbody2D>().linearVelocity = currentVelocity + knockBackVector / knockBackResistance;
            //     }
            // }




            //other.GetComponent<Rigidbody2D>().AddForce(knockBackVector * knockBackMultiplier);
            if (other.tag == "Enemy")
                other.GetComponent<LegEnemyMove>().hurt();
            else
                other.GetComponent<BossController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Puzz2") && gameObject.CompareTag("FBAT"))
        {
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Puzz") && !gameObject.CompareTag("FBAT") && !gameObject.CompareTag("FARM"))
        {
            gameObject.SetActive(false);
        }
        else if(other.CompareTag("Arm") || other.CompareTag("Leg") || other.CompareTag("Battery"))
        {
            
        }
        else
        {
            //Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
        
    }
}
