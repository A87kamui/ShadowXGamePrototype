using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyController : MonoBehaviour
{
    //Access to components
    public GameObject player;
    private Rigidbody objectRb;
    private Transform target;
    [SerializeField] private Vector3 lookDirection;
    [SerializeField] private Animator enemyAnim;    //Reference object's animation
    private GameManager gameManager;

    public Transform[] markers; //Transform array of markers

    //Created variables
    public float rotationSpeed;
    public float facing;
    [SerializeField] private float runSpeed;
    [SerializeField] private float attackHitRange;  //radiusOfSatisfaction
    public bool isAlive;
    private int foundPlayer;
    public bool stealthKilled;

    //marker variables
    private int currentMarkerIndex; //Tracks current marker
    [SerializeField] private float patrolSpeed; //Patrol speed
    [SerializeField] private float threshold; //Patrol threshold distance from marker
    [SerializeField] private float waitTimeLimit = 3.0f; //waitlimit in seconds
    [SerializeField] private float waitCounter = 0.0f;  //track how long waiting
    [SerializeField] private bool waiting = false;  //tracks waiting status

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();  //Access to gameManager component

        //speed = 6.0f;
        rotationSpeed = 8.0f;
        runSpeed = 2.0f;
        objectRb.drag = 0.8f;
        attackHitRange = 1.29f;
        isAlive = true;
        foundPlayer = 0;
        stealthKilled = false;

        //Access to player object
        player = GameObject.Find("Female Ninja");

        patrolSpeed = 1.5f;
        threshold = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            //print("Enemy Speed: " + enemyAnim.GetFloat("Speed"));
            //Store vector of (player Position - enemy position)
            Vector3 playerDirection = (player.transform.position - transform.position);
            //facing = 1, 0, -1 as facing each other, perpendicular facing or facing same direction.
            facing = Vector3.Dot(playerDirection, transform.forward);

            //check if facing the player and distance is less than or equal to 5.of
            if (facing >= 0 && playerDirection.magnitude <= 5.0f)
            {
                //Debug.Log("Player Target: " + target);
                SetTarget(player.transform);
                FaceTarget();
                moveToTarget();
                foundPlayer++;
                if (foundPlayer == 1)
                {
                    if (gameObject.tag == "Enemy"
                         && GetComponent<HealthBar>().healthSlider.value < GetComponent<HealthBar>().maxHealth)
                    {
                        gameManager.notDetectedPoint = 0;
                        gameManager.detectedPoint -= 20;
                        //print("Spotted point: " + gameManager.spottedPoint);
                    }
                }
                //print("Update Distance: " + playerDirection.magnitude);
                if (playerDirection.magnitude < 1.3f)   //attackHitRange + 0.01
                {
                    MeleeAttack();
                }
            }
            else if (target == player.transform.Find("ProjectileSpawnPoint"))
            {
                waiting = true;
                if (waiting)
                {
                    enemyAnim.SetFloat("Speed", 0f);
                    
                    waitCounter += Time.deltaTime; //increase waitCounter by 1 second
                    if (waitCounter < waitTimeLimit)
                    {
                        //print("Target: " + target);
                        enemyAnim.SetTrigger("isAlert");
                        return;
                    }
                    if (waitCounter < waitTimeLimit + 3.0f)
                    {
                        //print("Target: " + target);
                        enemyAnim.SetTrigger("isAlert");
                        FaceTarget();
                        return;
                    }
                    waiting = false;
                    waitCounter = 0;
                    SetTarget(markers[currentMarkerIndex]);
                }
            }
            else 
            {
                //Desbug.Log("PatrolMovement with target: " + target);
                SetTarget(markers[currentMarkerIndex]);
                FaceTarget();
                PatrolMovement();
                foundPlayer = 0;
                //print("Not spotted: " + foundPlayer);
                //print(projectileAttacked);
            }//*/
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
        //Debug.Log("Target: " + target);
    }

    private void FaceTarget()
    {
        enemyAnim.SetFloat("Speed", 0f);

        //Store vector of (target Position - enemy position)
        Vector3 lookDirection = (target.transform.position - transform.position);

        if (target == player.transform && lookDirection.magnitude <= 5.0f)
        {        
            //Rotate object to look at player
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            //Set enemy rotation speed from current position to target
            transform.rotation = Quaternion.Lerp(objectRb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //Zero out the rotation for x-axis and z-axis
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }//*/
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            //Set enemy rotation speed from current position to target
            transform.rotation = Quaternion.Lerp(objectRb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //Zero out the rotation for x-axis and z-axis
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
    }

    private void PatrolMovement()
    {
        if (waiting)
        {
            enemyAnim.SetFloat("Speed", 0f);
            waitCounter += Time.deltaTime; //increase waitCounter by 1 second
            if(waitCounter < waitTimeLimit)
            {
                return;
            }
            waiting = false;
        }

        enemyAnim.SetFloat("Speed", patrolSpeed);

        if (Vector3.Distance(transform.position, markers[currentMarkerIndex].position) <= threshold)
            {
            waitCounter = 0;
            waiting = true;
            currentMarkerIndex = (currentMarkerIndex + 1) % markers.Length;
        }//*/        
    }

    private void moveToTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > attackHitRange)
        {
            //print("Running to player");
            enemyAnim.SetFloat("Speed", runSpeed);
        }       
    }

    public void MeleeAttack()
    {
        //Set enemy animation to do melee attack
        Vector3 playerDirection = (player.transform.position - transform.position);
        enemyAnim.SetTrigger("Melee Attack");
        //print("Melee Distance: " + playerDirection.magnitude);
        //print("isAlive: " + isAlive);
    }

    public void CheckForHit()
    {
        //print("Hit Distance: " + Vector3.Distance(transform.position, player.transform.position));
        if (player == null)
        {
            return;
        }
        if (isAlive)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < attackHitRange)
            {
                //print("Successful hit!");
                if (gameObject.CompareTag("Enemy"))
                {
                    player.GetComponent<HealthBar>().TakeDamage(2);
                    player.GetComponent<PlayerController>().PlayerAttacked();
                    //print("Enemy damage: 2");
                }
                if (gameObject.CompareTag("Boss"))
                {
                    player.GetComponent<HealthBar>().TakeDamage(4);
                    player.GetComponent<PlayerController>().PlayerAttacked();
                    //print("Boss damage: 4");
                }
            }//*/
        } 
    }

    public void EnemyAttacked()
    {
        if (GetComponent<HealthBar>().healthSlider.value >= GetComponent<HealthBar>().maxHealth
            && isAlive == true)
        {
            isAlive = false;

            enemyAnim.SetFloat("Speed", 0);
            //SetBool takes the name of the boolean to set, and value T/F
            enemyAnim.SetBool("isDead", true);
            //print("isDead: " + enemyAnim.GetBool("isDead"));           
            //print("Enemy attacked. isAlive: " + isAlive);
            if (stealthKilled == false)
            {
                gameManager.detectedKillPoint += 5;
                /*print("Detected Kill Point: " + gameManager.detectedKillPoint);
                print("Stealth Kill: " + stealthKilled);//*/
            }
        }
        else if (GetComponent<HealthBar>().healthSlider.value > 0
            && GetComponent<HealthBar>().healthSlider.value < GetComponent<HealthBar>().maxHealth)
        {
            enemyAnim.SetTrigger("isAttacked");
            enemyAnim.SetTrigger("isAlert");
            //print("Enemy Attacked");
            if (gameObject.tag == "Boss")
            {
                SetTarget(player.transform);
                FaceTarget();
                moveToTarget();
            }
        }//*/
        enemyAnim.SetTrigger("isAlert");
    }

    //Enter a trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);  //destory power up
            GetComponent<HealthBar>().TakeDamage(2); //Reduce healthbar
            enemyAnim.SetTrigger("isAttacked");
            enemyAnim.SetTrigger("isAlert");
            SetTarget(player.transform.Find("ProjectileSpawnPoint"));
            //print("Powerup attacked");
        }
    }//*/

    /*//Checks if enemy is colliding with something (ground/obstacle)
    void OnCollisionEnter(Collision collision)
    {

    }//*/
}
