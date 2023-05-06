using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    //Variables access to components
    private Rigidbody playerRb;
    [SerializeField] private Animator playerAnim;    //Reference player's animation
    public GameObject projectilePrefab; //Store prefab object
    public Transform projectileSpawnPoint;
    public GameObject projectileSpawner;
    private GameObject target;
    private GameManager gameManager;
    public Camera mainCamera;
    public Camera aimCamera;

    //Created Variables
    [SerializeField] private float rotationSpeed;    //Rotation speed
    [SerializeField] private float walkSpeed;    //walkSpeed
    [SerializeField] private float runSpeed;    //runSpeed

    //Jump
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpForwardForce;
    public bool isOnGround = true;  //Check if player is on ground
    public bool isJumping = false;

    //Shuriken
    public int shurikenCount;
    [SerializeField] private float projectileSpeed;   //Movement speed
    [SerializeField] private GameObject targetRing;

    //Attack
    [SerializeField] private float attackHitRange;
    [SerializeField] private float cooldown;    //Projectile cooldown
    private int attackCount;
    [SerializeField] private float facing;

    //Sound
    public AudioClip hitSound;
    public AudioClip hurtSound;
    public AudioSource playerAudio; //*/

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();   //Rigidbody component access
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();  //Access to gameManager component

        rotationSpeed = 100f;
        walkSpeed = 50.0f;
        runSpeed = 125.0f;

        jumpForce = 6.8f;
        jumpForwardForce = 1.5f;

        shurikenCount = 0;
        projectileSpeed = 10.0f;

        attackHitRange = 1.27f;
        cooldown = 1.5f;
        attackCount = 0;

        playerAudio = GetComponent<AudioSource>();

        //GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            ProjectileAttack();
            MeleeAttack();
            if (playerRb.velocity.y == 0)
            {
                isOnGround = true;
            }
            if (Input.GetMouseButtonDown(1) && shurikenCount > 0)
            {
                mainCamera.enabled = !mainCamera.enabled;
                aimCamera.enabled = !aimCamera.enabled;
                targetRing.SetActive(true);

                /*print("main camera: " + mainCamera.enabled);
                print("aim camera: " + aimCamera.enabled);//*/
            }
            if (shurikenCount == 0)
            {
                mainCamera.enabled = true;
                aimCamera.enabled = false;
                targetRing.SetActive(false);
            }
            CheatCode();
        }       
        //print("Player velocity y: " + playerRb.velocity.y);
    }

    /// <summary>
    /// Player movement
    /// </summary>
    public void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {

        //Get player vertical input        
        float forwardInput = Input.GetAxis("Vertical");
        //Store User horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        //Check if jump buttons is pressed
        if (Input.GetAxis("Jump") > 0)// GetKeyDown(KeyCode.Space))
        {
            //print("velocity y: " + playerRb.velocity.y);
            playerAnim.SetBool("isJumping", true);
            if (isOnGround)
            {
                playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
                isOnGround = false;
                playerAnim.SetBool("Grounded", false);
                //print("Grounded bool: " + playerAnim.GetBool("Grounded"));

                if (Input.GetKey(KeyCode.W))
                {
                    playerRb.AddForce(transform.forward * jumpForwardForce, ForceMode.Impulse);
                    //print("Forward force applied");
                }

                //print("Player velocity y: " + playerRb.velocity.y);
                if (isJumping && playerRb.velocity.y <= 4.0f)
                {
                    playerAnim.SetBool("isFalling", true);
                    playerAnim.SetBool("isJumping", false);
                }
            }
        }

        //Walking
        if (forwardInput == 1 && isOnGround)
        {
            playerAnim.SetFloat("Speed", 1.2f);
            playerAnim.SetBool("isFalling", false);
            Vector3 velocity = (transform.forward * forwardInput) * walkSpeed * Time.deltaTime;
            velocity.y = playerRb.velocity.y;
            playerRb.velocity = velocity;

            /*print("forwardInput: " + forwardInput);
            print("Anim Speed: " + playerAnim.GetFloat("Speed"));
            print("Player walk velocity: " + playerRb.velocity);//*/

            //Running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnim.SetBool("Run", true);
                velocity = (transform.forward * forwardInput) * runSpeed * Time.deltaTime;
                velocity.y = playerRb.velocity.y;
                playerRb.velocity = velocity;//*/
                /*print("WalkSpeed: " + walkSpeed);
                print("RunSpeed: " + runSpeed);
                print("Player run velocity: " + playerRb.velocity);//*/
            }
            else
            {
                playerAnim.SetBool("Run", false);
                //print("WalkSpeed: " + walkSpeed);
            }
        }
        else
        {
            playerAnim.SetFloat("Speed", 0.0f);
            playerAnim.SetBool("Run", false);
            /*print("forwardInput: " + forwardInput);
            print("Run: " + playerAnim.GetBool("Run"));//*/
        }

        //Rotate around y-axis with input at rotationSpeed per second
        playerRb.transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Player projectile attack
    /// </summary>
    public void ProjectileAttack()
    {
        //Attack Projectile 
        if (Input.GetMouseButtonUp(1) && shurikenCount > 0 && cooldown < 1.0f)
        {
            mainCamera.enabled = !mainCamera.enabled;
            aimCamera.enabled = !aimCamera.enabled;
            targetRing.SetActive(false);

            //Instantiate = create copies of objects already created
            var shuriken = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
            //Set velocity to forward direction at projectileSpeed 
            shuriken.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.forward * projectileSpeed;

            shurikenCount--;
            gameManager.UpdateShurikenCount(-1);
            //Debug.Log("Pressed right click.");
            cooldown = 1.5f;
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        //print("Projectile");
    }

    /// <summary>
    /// Player melee attack
    /// </summary>
    public void MeleeAttack()
    {
        if (attackCount == 0)
        {
            attackCount++;
            return;
        }

        //Attack Melee 
        if (Input.GetMouseButtonUp(0) && attackCount != 0)
        {
            //Set player animation to do melee attack
            playerAnim.SetTrigger("Melee_Attack");
        }
        //print("Melee");
    }

    /// <summary>
    /// Player check for hit in animation
    /// </summary>
    public void CheckForHit()
    {
        if (target == null)
        {
            return;
        }

        //print("Check for Hit");

        Vector3 targetDirection = (transform.position - target.transform.position);
        //facing = 1, 0, -1 as facing each other, perpendicular facing or facing same direction.
        facing = Vector3.Dot(targetDirection, transform.forward);
        //facing = Vector3.Dot(transform.forward, targetDirection);

        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        //print("Target Distance: " + targetDistance);

        //print("Player Facing: " + facing);
        //print("Enemy Facing: " + target.GetComponent<EnemyController>().facing);
        /*if (facing < -0.4)
        {
            print("Target is in front of me" + facing);
            print("Distance: " + targetDistance);     
        }
        else
        {
            print("Not in front of me: " + facing);
            print("Distance: " + targetDistance);
        }//*/

        if (target.GetComponent<EnemyController>().facing < 0
            && target.gameObject.tag == "Enemy" && target.GetComponent<EnemyController>().isAlive)
        {
            target.GetComponent<HealthBar>().TakeDamage(target.GetComponent<HealthBar>().maxHealth);
            target.GetComponent<EnemyController>().stealthKilled = true;
            target.GetComponent<EnemyController>().EnemyAttacked();
            gameManager.stealthKillPoint += 20;
            playerAudio.PlayOneShot(hitSound, 1.0f);
            //("Stealth Kill");
        }
        else if (targetDistance < attackHitRange && facing < -0.4)
        {
            target.GetComponent<HealthBar>().TakeDamage(3);
            target.GetComponent<EnemyController>().EnemyAttacked();
            playerAudio.PlayOneShot(hitSound, 1.0f);
            //print("Successful hit!");
        } //*/
    }

    /// <summary>
    /// Check if player is attacked
    /// Calls animation for player getting hit
    /// Calls animation for when player dies
    /// </summary>
    public void PlayerAttacked()
    {
        //print("Player attacked");
        if (GetComponent<HealthBar>().healthSlider.value >= GetComponent<HealthBar>().maxHealth)
        {
            playerAnim.SetFloat("Speed", 0);
            //SetBool takes the name of the boolean to set, and value T/F
            playerAnim.SetBool("isDead", true);
            playerAudio.PlayOneShot(hitSound, 1.0f);
            gameManager.GameOver();
            //print("Died");
        }
        else if(GetComponent<HealthBar>().healthSlider.value > 0
            && GetComponent<HealthBar>().healthSlider.value < GetComponent<HealthBar>().maxHealth)
        {
            playerAnim.SetTrigger("isAttacked");
            playerAnim.SetTrigger("isReady");
            playerAudio.PlayOneShot(hitSound, 1.0f);
        }
    }

    /// <summary>
    /// Checks if player is colliding with gameobject (ground/obstacle/enemy)
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        //Check if player collides with object with ground tag
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle")
            || collision.gameObject.CompareTag("Enemy"))
        {
            isOnGround = true;
            playerAnim.SetBool("Grounded", true);

            isJumping = false;
            playerAnim.SetBool("isJumping", false);

            playerAnim.SetBool("isFalling", false);
            /*print("Colliding with: " + collision);
            print("isFalling: " + playerAnim.GetBool("isFalling"));//*/
        }
        if (collision.gameObject.CompareTag("KeyItem"))
        {
            Destroy(collision.gameObject);
            gameManager.MissionCompleteMenu();
        }
    }

    /// <summary>
    /// Check if player is exiting a collision with a gameobject (ground/obstacle/enemy)
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        //print("is on ground: " + isOnGround);
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle")
            || collision.gameObject.CompareTag("Enemy"))
        {
            //print("is on ground: " + isOnGround);

            if (isJumping && playerRb.velocity.y == 0.0f || playerRb.velocity.y < 0.0f)
            {
                //print("velocity y: " + playerRb.velocity.y);
                playerAnim.SetBool("isFalling", true);
            }
        }
    }

    /// <summary>
    /// Enter a trigger--make sure objects have Is trigger check on their collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            shurikenCount += 5;
            gameManager.UpdateShurikenCount(5);

            if (shurikenCount > 99)
            {
                shurikenCount = 99;
            }

            Destroy(other.gameObject);
            //print("Player shuriken count: " + shurikenCount);
        }

        if (other.gameObject.CompareTag("Indicator"))
        {
            Destroy(other.gameObject);
        }


        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            target = other.gameObject;
            //print("Target set");
        }//*/

        if (other.gameObject.CompareTag("Health"))
        {
            if (GetComponent<HealthBar>().healthSlider.value == 0)
            {
                //print("Return");
                return; //Dont pick up health
            }
            else
            {
                //print("Restore Health");
                GetComponent<HealthBar>().TakeDamage(-10);
                Destroy(other.gameObject);
            }           
        }
    }

    /// <summary>
    /// Cheat code
    /// </summary>
    public void CheatCode()
    {
        //destory all enemies
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.K))
        {
            //print("cheat code 1");
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
            {
                if(obj.gameObject.tag == "Enemy")
                {
                    Destroy(obj.gameObject);
                }
                
            }
        }

        //Restore 10HP
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.L))
        {
            //print("cheat code 2");
            GetComponent<HealthBar>().TakeDamage(-10);
        }

        //Add 99 Shurikens to player
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            //print("cheat code 3");
            shurikenCount += 99;
            gameManager.UpdateShurikenCount(99);

            if (shurikenCount > 99)
            {
                shurikenCount = 99;
            }
            //print("player shuriken count: " + shurikenCount);
        }
    }
}
