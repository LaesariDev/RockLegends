using JetBrains.Annotations;
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float airMultiplier = 0.4f;

    [Header("Maa tsekkaus")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [SerializeField]
    Transform orientation;
    [SerializeField]
    Camera mainCam;

    float horizontalInput;
    float verticalInput;

    bool isSprinting = false;

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
        grounded = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, 0.75f, whatIsGround);
        

        // Function calling
        myInput();
        SpeedControl();
        Sprinttaus();

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

    private void Sprinttaus()
    {
        // CTRL Chekki
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        // Nopeuden muutos sprintatessa
        if(isSprinting)
        {
            moveSpeed = 6.5f;
            StartCoroutine(FovChange(80));
        }
        else
        {
            moveSpeed = 3.5f;
            //StartCoroutine(FovChange(70));
        }

    }

    private IEnumerator FovChange(float newFow)
    {
        float oldFov = mainCam.fieldOfView;

        while(oldFov > newFow)
        {
            newFow++;
            yield return new WaitForSeconds(0.5f);
            
        }

        //while (oldFov > newFow)
        //{
        //    newFow--;
        //    yield return new WaitForSeconds(0.05f);
       // }

        mainCam.fieldOfView = newFow;
    }


    // Hyppy funktio
    private void Jump()
    {
        // Resetoi y liike
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }



}
