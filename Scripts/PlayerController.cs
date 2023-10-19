using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private LayerMask groundLayerMask;
    private Rigidbody2D rb;
    private Collider2D collider;
    //Actions
    public InputAction moveAction;
    public InputAction flipGravityAction;
    public InputAction funnyFlipAction;
    
    
    public bool grounded;
    public float horizontalInput;
    public float speed = 7.0f;
    public Vector2 velocity;
    public float gravityStrength = -10;

    void Start ()
    {
        // playerAnimationController = GetComponent<PlayerAnimationController>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        screenFader = FindObjectOfType<ScreenFader>();

        moveAction.Enable();
        flipGravityAction.Enable();
        
        velocity = Vector3.zero;
    }

    void Update ()
    {
        horizontalInput = moveAction.ReadValue<float>(); //read horizontal movement
        velocity.x = horizontalInput * speed;

        grounded = IsGrounded();
        if (flipGravityAction.triggered && IsGrounded()) //check if flip gravity triggered, and player is grounded
        {
            FlipGravity();
        }

        // velocity.y += gravityStrength * Time.deltaTime; //Traditional Gravity
        // //Max velocity checks
        // if (velocity.y > maxVelocity) velocity.y = maxVelocity;
        // if (velocity.y < -maxVelocity) velocity.y = -maxVelocity;
        velocity.y = gravityStrength; //No acceleration gravity, more like VVVVVV
        rb.velocity = velocity; //move object
    }

    //Can only flip gravity when grounded
    void FlipGravity()
    {
        gravityStrength *= -1; //reverse gravity
        playerAnimationController.TriggerJumpAnim();
        // velocity.y = 0; //Add small boost
    }

    public bool IsGrounded()
    {
        float extraHeightTest = 0.2f; //Gives you a little bit of room to remain grounded
        var bounds = collider.bounds;
        
        //Get ray direction
        Vector3 raydirection = Vector3.zero;
        if (gravityStrength < 0)
        {
            raydirection.y = -1;
        }
        else
        {
            raydirection.y = 1;
        }

        //draw raycast
        bool raycastHit = Physics2D.Raycast(bounds.center, raydirection, bounds.extents.y + extraHeightTest);
        Color rayColour;
        if (raycastHit)
        {
            rayColour = Color.green;
        }
        else
        {
            rayColour = Color.red;
        }
        Debug.DrawRay(bounds.center, raydirection * (bounds.extents.y + extraHeightTest));
        return raycastHit;
    }
}
