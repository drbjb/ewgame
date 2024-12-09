using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    private void onTriggerEnter(Collider collider){
        print("should be murder2");
        if (collider.GetComponent<Health>() != null){
            print("should be murder");
            Health h = collider.GetComponent<Health>();
            h.Damage(damage);
        }
        
    }
}
