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
        //print("prowl");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            /*
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            */
            ///End of attack code
            //print("attack");
            //actually just the script for the plater as a whole.. ik its stupid :/ but i dont wanna fix it
            //cuz im lazy
            playerMovement.health -= 1;
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
}
