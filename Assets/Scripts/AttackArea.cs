using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;
    public float speed = 1000000;

    public void update()
    {
        transform.rotation = transform.parent.transform.rotation; 
    }
    private void OnTriggerEnter(Collider collider)
    {
        print("should be murder2");
        if (collider.CompareTag("Enemy"))
        {
            if (collider.GetComponent<Health>() != null)
            {
                print("should be murder");
                Health h = collider.GetComponent<Health>();
                if (h)
                    h.Damage(damage);

                Enemy e = collider.GetComponent<Enemy>();
                Vector3 direction = (transform.parent.transform.position - collider.transform.position).normalized;
                Debug.DrawLine(transform.position, collider.transform.position, Color.red, 1f);
                e.Kback(direction);  // Pass direction to Kback without multiplying by -1
                print(direction);
            }
        }

        if (collider.CompareTag("PowerUp"))
        {
            print("should add to health");

            PlayerMovement pm = GetComponentInParent<PlayerMovement>();
            pm.health += 5;
        }
    }
    /*
    private void OnTriggerEnter(Collider collider){
        print("should be murder2");
        if (collider.CompareTag("Enemy"))
        {
            if (collider.GetComponent<Health>() != null)
            {
                print("should be murder");
                Health h = collider.GetComponent<Health>();
                if (h)
                    h.Damage(damage);
                Enemy e = collider.GetComponent<Enemy>();
                Vector3 direction = (transform.parent.transform.position - collider.transform.position).normalized;
                Debug.DrawLine(transform.position, collider.transform.position, Color.red, 1f);
                e.Kback(direction);
                print(direction);
                
            }
        }

        
    }
    */
}
