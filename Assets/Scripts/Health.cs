using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0){
            Destroy(gameObject);
        }
        
    }

    public void Damage(int d){
        health -= d;
    }
}