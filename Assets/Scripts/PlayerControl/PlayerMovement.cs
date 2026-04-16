using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;

    [Header("Maa tsekkaus")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [SerializeField]
    Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDir;
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    
    private void Update()
    {
        // Maa tsekki rivi
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Function calling
        myInput();
        SpeedControl();

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0f;
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }


    // Input tsekit
    private void myInput()
    {
        // Input muuttujat axiseista
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            Jump();
        }

    }


    // Liikuttamis funktio
    private void movePlayer()
    {
        // Lasketaan liikkumissuunta
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Lisätään voimaa suuntaan
        rb.AddForce(moveDir * moveSpeed * 10f, ForceMode.Force);

    }


    // Nopeuden limittaus
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // Rajoita tarvittaessa
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z); 
        }
    }


    // Hyppy funktio
    private void Jump()
    {
        // Resetoi y liike
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

}
