using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float airMultiplier = 0.4f;

    [Header("Maa tsekkaus")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform groundCheck;

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
        grounded = Physics.SphereCast(groundCheck.position, 0.3f, Vector3.down, out RaycastHit hit, 0.4f, whatIsGround);

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

        if (grounded)
        {
            Debug.Log("Toimii");
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
        if (grounded)
        {
            rb.AddForce(moveDir * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDir * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

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
