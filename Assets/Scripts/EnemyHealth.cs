using UnityEngine;

public class Health : MonoBehaviour
{
    public int ehealth = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ehealth <= 0){
            Destroy(gameObject);
        }
        
    }

    public void Damage(int d){
        ehealth -= d;
        print("monster: " + ehealth);
    }
}
