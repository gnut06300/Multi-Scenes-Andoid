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

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        jumpButton = FindObjectOfType<JumpController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, layerIsGround);
        
        if (isGrounded && jumpButton.isPressed)
        {
            Debug.Log("Yolo");
        }
    }
}
