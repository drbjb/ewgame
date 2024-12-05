using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<Health>() != null){
            print("should be murder");
            Health h = collider.GetComponent<Health>();
            h.Damage(damage);
        }
        
    }
}
