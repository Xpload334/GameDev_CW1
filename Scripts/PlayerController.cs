using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Connections")]
    [SerializeField] private ScreenFader screenFader; 
    [SerializeField] private PlayerAnimationController playerAnimationController;
    private Rigidbody2D _rb;
    [SerializeField] private Collider2D myCollider;
    [Header("Actions")]
    public InputAction moveAction; //Action for moving player horizontally
    public InputAction flipGravityAction; //Action for flipping gravity
    public float horizontalInput; //float for horizontal input
    [Header("Physics")]
    [SerializeField] private LayerMask groundLayerMask; //What layer to use for ground tiles
    public Vector2 desiredGravity = new(0, -50f); //Vector for gravity
    [SerializeField] private float flipBoostStrength = 5f; //Boost in velocity to give when flipping
    [SerializeField] private float terminalVelocity = 20f; //Max velocity
    public float playerSpeed = 7.0f;
    private Vector2 _velocity;

    void Start ()
    {
        // playerAnimationController = GetComponent<PlayerAnimationController>();
        _rb = GetComponent<Rigidbody2D>();
        // _collider = GetComponent<Collider2D>();
        screenFader = FindObjectOfType<ScreenFader>();

        moveAction.Enable();
        flipGravityAction.Enable();
        
        //set gravity
        Physics2D.gravity = desiredGravity;
        _velocity = Vector3.zero;
    }

    void Update ()
    {
        horizontalInput = moveAction.ReadValue<float>(); //read horizontal movement
        _velocity.x = horizontalInput * playerSpeed; //Set horizontal velocity

        IsGrounded();
        if (flipGravityAction.triggered && IsGrounded()) //check if flip gravity triggered, and player is grounded
        {
            FlipGravity();
        }
        _velocity.y = _rb.velocity.y;

        //Terminal velocity
        if (_velocity.y > terminalVelocity) _velocity.y = terminalVelocity;
        if (_velocity.y < -terminalVelocity) _velocity.y = -terminalVelocity;
        
        _rb.velocity = _velocity; //move object
        
        
        //Animations
        playerAnimationController.AnimateMovement(horizontalInput);
    }

    //Can only flip gravity when grounded
    void FlipGravity()
    {
        //reverse gravity direction
        desiredGravity.y = -desiredGravity.y;
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, desiredGravity.y);
        
        //Provide small boost
        _velocity.y = desiredGravity.normalized.y * flipBoostStrength;
        
        //Do anims
        playerAnimationController.TriggerJumpAnim();
    }

    //Check if player is grounded
    public bool IsGrounded()
    {
        float extraHeightTest = 0.2f; //Gives you a little bit of room to remain grounded
        var bounds = myCollider.bounds;
        
        //Get ray direction
        var rayDirection = Vector2.zero;
        if (Physics2D.gravity.y < 0) //Use current gravity direction
        {
            rayDirection.y = -1;
        }
        else
        {
            rayDirection.y = 1;
        }

        //draw raycast
        bool raycastHit = Physics2D.Raycast(bounds.center, rayDirection, bounds.extents.y + extraHeightTest, groundLayerMask);
        var rayColour = raycastHit ? Color.green : Color.red; //Green if hits ground, red if doesn't
        Debug.DrawRay(bounds.center, rayDirection * (bounds.extents.y + extraHeightTest), rayColour);
        return raycastHit;
    }
}
