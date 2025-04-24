using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
     public NavMeshAgent agent;

    public Rigidbody rb;

    public Transform player;

    public GameObject playerO;

    public CapsuleCollider checkz;
    private PlayerMovement playerMovement;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    bool back = false;
    double backtime = 0;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public int damage;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        playerMovement = playerO.GetComponent<PlayerMovement>();
        playerMovement.health -= 1;
        checkz = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (backtime <= 0)
        {
            agent.enabled = true;
            rb.isKinematic = true;
            //print("should return...");
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
        


        if (Input.GetMouseButtonDown(0)){
            print(back);
            //Attack();
        }
        backtime -= Time.deltaTime;
    }

    private void Patroling()
    {
        //print("patrolling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Update the agent's destination
        agent.SetDestination(player.position);

        // Make sure the enemy faces the player while moving
        FacePlayer();
    }

    private void AttackPlayer()
    {
        // Stop the enemy from moving
        agent.SetDestination(transform.position);

        // Face the player
        FacePlayer();

        // If the enemy is not in the middle of an attack, perform the attack
        if (!alreadyAttacked)
        {
            playerMovement.health -= damage;
            playerMovement.HBar();
            print(playerMovement.health);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    /*
    public void Kback(Vector3 direction)
    {
        rb.AddForce(direction * 600 *-1);
        backtime = 1;
        print("knockack!");
        agent.enabled = false;
        rb.isKinematic = false;

    }
    */
    public void Kback(Vector3 direction, float aura, int btime)
    {
        // Remove the negative multiplier here to push the enemy away from the player
        rb.AddForce(direction * -4.5f * aura, ForceMode.Impulse);  // Apply force in the correct direction

        backtime = 0.3 * btime;
        print("knockback!");

        // Disable the NavMeshAgent temporarily during knockback
        agent.enabled = false;

        // Ensure Rigidbody is not kinematic
        rb.isKinematic = false;

        FacePlayer();
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void FacePlayer()
    {
        // Calculate the direction to the player (ignore the Y-axis for 2D effect)
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Prevent vertical rotation (keep it purely 2D)

        // If the direction is non-zero, rotate the plane to face the player
        if (direction.magnitude > 0.1f)  // Add a small threshold to prevent jittering
        {
            // Directly set the rotation to look at the player
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}


