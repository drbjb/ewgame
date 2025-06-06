using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    public float moveSpeed;

    public float groundDrag; 
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput; float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    public int health;
    public int maxHealth = 15;

    public HealthBar HealthBar;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        MyInput();
        SpeedControl();
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
        
        if(health <= 0){
            SceneManager.LoadScene("Death", LoadSceneMode.Single);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
  

    }

    private void FixedUpdate(){
        MovePlayer();
    }

    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x,0f, rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    public void HBar()
    {
        HealthBar.setHealth(health);
    }
}
