using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{

    [SerializeField] Joystick joystick;
    [SerializeField] JumpController jumpButton;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask layerIsGround;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        jumpButton = FindObjectOfType<JumpController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, layerIsGround);
        /*if (isGrounded)
        {
            GetComponent<CharacterController>().enabled = false;
        }*/
        /*if (jumpButton.isPressed)
        {
            GetComponent<CharacterController>().enabled = false;
        }*/
        //rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if (isGrounded && jumpButton.isPressed)
        {
            GetComponent<CharacterController>().enabled = false;
            //rb.velocity = Vector3.up * jumpForce;
        }
    }
}
