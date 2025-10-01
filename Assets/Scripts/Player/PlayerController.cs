using UnityEngine;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;       // Normal movement speed
    public float dashSpeed = 15f;      // Speed during dash
    public float dashDuration = 0.2f;  // How long the dash lasts
    public float dashCooldown = 1f;    // Time before you can dash again
    public int armCount = 0; // Number of arms
    public int legCount = 0; // Number of Legs
    public int batCount = 0; // Number of batteries
    public int brainCount = 1;//Number of brains
    private Rigidbody2D rb; // for getting rigid body
    private float moveInput; 
    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastDash = -999f;
    public GameObject armShot;
    public GameObject batShot;
    public GameObject legShot;
    public GameObject brainShot;
    private int dire = 0;
    private Component mik;
    public int playerhealth;
    public float healtime;
    public float invistime;
    private bool CanControlPlayer => brainCount > 0 && playerhealth > 0;

    private List<GameObject> recentCollison = new List<GameObject>();

    [Header("Health Sprites")]
    public Sprite[] slimesprites;
    public UnityEngine.UI.Image healthimage;

    [Header("Lost Health Sprites")]
    public GameObject[] damageOverlays;

    public GameObject GameOverImage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        brainCount = 1;
        InventoryManager.Instance.UpdateUIFromPlayer(this);
        UpdateHealthUI();
        UpdatedamageOverlays();
    }
    void UpdateHealthUI()
    {
        // Ensure index is within bounds of your slimesprites array
        int index = Mathf.Clamp(playerhealth, 0, slimesprites.Length - 1);
        healthimage.sprite = slimesprites[index];
    }

    void UpdatedamageOverlays()
    {
        int lost = Mathf.Clamp(4 - playerhealth, 0, damageOverlays.Length);
        for (int i = 0; i < damageOverlays.Length; i++)
        {
            damageOverlays[i].SetActive(i < lost);
        }
    }

    void Update()
    {
        if (playerhealth <= 0)
        {
            if (GameOverImage != null)
            {
                GameOverImage.SetActive(true);
            }
             return;
            
        }
        if (!CanControlPlayer) return;
        // Get left/right input (-1 to 1)
        moveInput = Input.GetAxisRaw("Horizontal");
        if (playerhealth != 4)
        {
            healtime += Time.deltaTime;
        }
        if (healtime > 30)
        {
            healtime = 0;
            playerhealth += 1;
            UpdateHealthUI();
            UpdatedamageOverlays();
        }
        float z = Input.GetAxis("Horizontal");
        if (z != 0)
        {
            if (z < 0)
            {
                dire = 0;
            }
            else
            {
                dire = 1;
            }
        }
        // Dash input (Shift key)
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDash + dashCooldown)
        {
            if (gameObject.GetComponent<AudioSource>().resource.name != "Blob_Dash")
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Blob_Dash");
            }
            gameObject.GetComponent<AudioSource>().Play();

            isDashing = true;
            dashTimeLeft = dashDuration;
            lastDash = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {



            if (armCount > 0)
            {

                if (gameObject.GetComponent<AudioSource>().resource.name != "Throw2")
                {
                    gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Throw2");
                }
                gameObject.GetComponent<AudioSource>().Play();

                armCount -= 1;
                InventoryManager.Instance.UpdateUIFromPlayer(this);
                GameObject newObject = Instantiate(armShot, transform.position, Quaternion.identity);
                gameObject.GetComponent<SpawnOnPlayer>().KillLimbArm();

                if (dire > 0)
                {
                    newObject.GetComponent<Fired>().z = 1;
                }
                else
                {
                    newObject.GetComponent<Fired>().z = 0;
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (batCount > 0)
            {

                if (gameObject.GetComponent<AudioSource>().resource.name != "Throw2")
                {
                    gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Throw2");
                }
                gameObject.GetComponent<AudioSource>().Play();

                batCount -= 1;
                InventoryManager.Instance.UpdateUIFromPlayer(this);
                GameObject newObject = Instantiate(batShot, transform.position, Quaternion.identity);
                gameObject.GetComponent<SpawnOnPlayer>().KillLimbBat();


                if (dire > 0)
                {
                    newObject.GetComponent<Fired>().z = 1;
                }
                else
                {
                    newObject.GetComponent<Fired>().z = 0;
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (legCount > 0)
            {

                if (gameObject.GetComponent<AudioSource>().resource.name != "Throw2")
                {
                    gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Throw2");
                }
                gameObject.GetComponent<AudioSource>().Play();


                legCount -= 1;
                InventoryManager.Instance.UpdateUIFromPlayer(this);
                GameObject newObject = Instantiate(legShot, transform.position, Quaternion.identity);
                gameObject.GetComponent<SpawnOnPlayer>().KillLimbLeg();

                if (dire > 0)
                {
                    newObject.GetComponent<Fired>().z = 1;
                }
                else
                {
                    newObject.GetComponent<Fired>().z = 0;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (brainCount > 0)
            {

                if (gameObject.GetComponent<AudioSource>().resource.name != "Throw2")
                {
                    gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Throw2");
                }
                gameObject.GetComponent<AudioSource>().Play();


                brainCount -= 1;
                playerhealth = 0;
                InventoryManager.Instance.UpdateUIFromPlayer(this);
                GameObject newObject = Instantiate(brainShot, transform.position, Quaternion.identity);
                gameObject.GetComponent<SpawnOnPlayer>().KillLimbBrain();

                if (dire > 0)
                {
                    newObject.GetComponent<Fired>().z = 1;
                }
                else
                {
                    newObject.GetComponent<Fired>().z = 0;
                }
                
            }
        }

        // if (Input.GetKeyDown(KeyCode.Alpha5))
        // {
        //     playerhealth -= 1;

        //     if (gameObject.GetComponent<AudioSource>().resource.name != "Hurt" || playerhealth != 0)
        //     {
        //         gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Hurt");
        //     }
        //     else if(gameObject.GetComponent<AudioSource>().resource.name != "Die")
        //     {
        //         gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Die");
        //     }
        //         gameObject.GetComponent<AudioSource>().Play();
        // }

    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Dash movement
            rb.linearVelocity = new Vector2(moveInput * (dashSpeed + legCount), rb.linearVelocity.y);
            dashTimeLeft -= Time.fixedDeltaTime;

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            // Normal movement
            rb.linearVelocity = new Vector2(moveInput * (moveSpeed + (legCount / 2)), rb.linearVelocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arm") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "PlayerBlob")
        {
            armCount += 1;

            if (gameObject.GetComponent<AudioSource>().resource.name != "Pickup")
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Pickup");
            }
            gameObject.GetComponent<AudioSource>().Play();
            
            
            Destroy(collision.gameObject);
            InventoryManager.Instance.UpdateUIFromPlayer(this);
        }
        if (collision.CompareTag("FARM") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "PlayerBlob")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.GetComponent<BoxCollider2D>());
        }
        if (collision.CompareTag("Leg") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "PlayerBlob")
        {
            legCount += 1;

            if (gameObject.GetComponent<AudioSource>().resource.name != "Pickup")
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Pickup");
            }
            gameObject.GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject);
            InventoryManager.Instance.UpdateUIFromPlayer(this);
        }
        if (collision.CompareTag("Battery") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "PlayerBlob")
        {
            batCount += 1;

            if (gameObject.GetComponent<AudioSource>().resource.name != "Pickup")
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Pickup");
            }
            gameObject.GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject);
            InventoryManager.Instance.UpdateUIFromPlayer(this);
        }
        if (collision.CompareTag("Lattack") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "PlayerBlob")
        {
            Destroy(collision);
            playerhealth -= 1;

            if (gameObject.GetComponent<AudioSource>().resource.name != "Hurt" || playerhealth != 0)
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Hurt");
            }
            else if(gameObject.GetComponent<AudioSource>().resource.name != "Die")
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Die");
            }
                gameObject.GetComponent<AudioSource>().Play();

            UpdateHealthUI();
            UpdatedamageOverlays();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.name == "PlayerBlob")
        {
            recentCollison.Add(collision.gameObject);

            float tim = Time.time;

            if (isDashing)
            {
                //Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                if (collision.gameObject.GetComponent<ArmEnemyMovement>() != null)
                {

                    //Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>(), false);
                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    collision.gameObject.GetComponent<ArmEnemyMovement>().modeChange();
                }
                else
                {
                    //Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>(), false);
                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    collision.gameObject.GetComponent<LegEnemyMove>().modeChange();
                    collision.gameObject.GetComponent<Animator>().SetInteger("mode", collision.gameObject.GetComponent<LegEnemyMove>().mode);
                }
            }

            float tim2 = 0f;
            tim2 = Time.time;
            while (tim2 - tim < 1f)
            {
                tim2 += Time.deltaTime;
                if (tim >= 1f)
                {
                    //Debug.Log("tim = " + tim);
                    for (int i = 0; i < recentCollison.Count; i++)
                    {
                        recentCollison[i].GetComponent<BoxCollider2D>().enabled = true;
                        Debug.Log("Enabled Box collider of " + recentCollison[i].name);
                    }
                    recentCollison.Clear();
                }
            }
        }
    }
}
