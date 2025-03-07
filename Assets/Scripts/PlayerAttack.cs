using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Camera playerCamera; // Assign your first-person camera in the inspector
    public float attackRange = 3f; // Adjust based on desired attack distance
    public int damage = 1;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    private float specialTime = 0f;
    public float specialTimeMax = 2;

    public PlayerMovement playerMovement;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            specialTime += Time.deltaTime;
            print(specialTime);
            print("HAIIII");
        }

        if (Input.GetKeyUp(KeyCode.E) && specialTime < specialTimeMax)
        {
            specialTime = 0;
        }
        if (Input.GetKeyUp(KeyCode.E) && specialTime > specialTimeMax)
        {
            damage = 100;
            Attack();
            specialTime = 0;
        }
    }

    private void Attack()
    {
        attacking = true;

        // Cast a ray forward from the camera's position
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, attackRange);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                // Apply damage
                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.Damage(damage);
                }

                // Apply knockback
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Vector3 knockbackDirection = playerCamera.transform.forward.normalized * -1;

                    enemy.Kback(knockbackDirection);
                }
            }
            if (hit.collider.CompareTag("PowerUp"))
            {
                // Apply damage
                PlayerMovement pm = GetComponentInParent<PlayerMovement>();
                pm.health += 5;

                Koolaid k = hit.collider.GetComponent<Koolaid>();
                k.done();

                pm.HBar();
                print(pm.health);
            }
        }



        // Visualize the attack ray
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * attackRange, Color.red, 0.1f);
    }
}

