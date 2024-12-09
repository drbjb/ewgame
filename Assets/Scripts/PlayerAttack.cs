using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject attackArea = default;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    void Start()
    {
        attackArea = transform.GetChild(3).gameObject;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            print("meow");
            Attack();
        }
            

        if(attacking){
            timer += Time.deltaTime;

            if(timer >= timeToAttack){
            
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }

            
        }
        

        
    }
    private void Attack(){
        attacking = true;
        attackArea.SetActive(attacking);
    }



}
