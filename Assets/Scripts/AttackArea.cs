using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;
    public float speed = 1000000;

    public void update()
    {
        transform.rotation = transform.parent.transform.rotation; 
    }
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
                Vector3 direction = (transform.position - collider.transform.position).normalized;
                e.Kback(direction);
                
            }
        }

        
    }
}
