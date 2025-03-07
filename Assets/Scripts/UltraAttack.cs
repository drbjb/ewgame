using UnityEngine;

public class UltraAttack : MonoBehaviour
{
    public float specialTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            specialTime += Time.deltaTime;
        }

        //if (Input.GetKeyUp(KeyCode.E) && specialTime < 2) {

    }
}
