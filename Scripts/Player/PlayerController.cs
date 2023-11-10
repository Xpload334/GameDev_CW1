using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Connections")]
    [SerializeField] private ScreenFader screenFader; 
    [SerializeField] private PlayerAnimationController playerAnimationController;

    [SerializeField] private PlayerSounds _playerSounds;
    public Rigidbody2D rb;
    [SerializeField] private Collider2D playerCollider;
    [Header("Actions")] 
    public bool canAct = true;
    public InputAction moveAction; //Action for moving player horizontally
    public InputAction flipGravityAction; //Action for flipping gravity
    private bool gravityFlipPress;
    public float horizontalInput; //float for horizontal input
    public float lastHorizontalInput;
    public UnityEvent onFlipGravityEvent;
    [Header("Physics")]
    public LayerMask groundLayerMask; //What layer to use for ground tiles
    [SerializeField] private float groundedHeightCheck = 0.05f;
    public Vector2 desiredGravity = new(0, -50f); //Vector for gravity
    [SerializeField] private float flipBoostStrength = 5f; //Boost in velocity to give when flipping
    [SerializeField] private float terminalVelocity = 20f; //Max velocity
    public float playerSpeed = 7.0f;
    [SerializeField] private float movementSmoothing = .05f;
    private Vector2 _mVelocity;
    private Vector2 _velocity;
    //Platform Interactions
    private bool _isOnPlatform;
    private bool _ignorePlatformPhysics;
    public Transform platformRBody;
    private Vector3 _lastPlatformPosition;
    //Coyote time
    private float airFlipTime = 0.1f;
    [SerializeField] private float airFlipTimeCounter;
    //Flip buffer
    private float flipBufferTime = 0.1f;
    [SerializeField] private float flipBufferTimeCounter;

    void Start ()
    {
        // playerAnimationController = GetComponent<PlayerAnimationController>();
        _playerSounds = FindObjectOfType<PlayerSounds>();
        rb = GetComponent<Rigidbody2D>();
        screenFader = FindObjectOfType<ScreenFader>();

        moveAction.Enable();
        flipGravityAction.Enable();

        flipGravityAction.started += _ =>
        {
            OnFlipGravityAction();
        };


        //set gravity
        rb.gravityScale = desiredGravity.y / Physics2D.gravity.y;
        _velocity = Vector3.zero;
    }

    void OnFlipGravityAction()
    {
        // Debug.Log("Flip action");
        flipBufferTimeCounter = flipBufferTime;
    }

    void FixedUpdate ()
    {
        bool isGrounded = IsGrounded();
        
        //Platform interactions
        if (_isOnPlatform)
        {
            Vector2 deltaPosition = platformRBody.position - _lastPlatformPosition;
            rb.position += deltaPosition;
            _lastPlatformPosition = platformRBody.position;
        }
        //Coyote time
        if (isGrounded)
        {
            airFlipTimeCounter = airFlipTime;
        }
        else
        {
            airFlipTimeCounter -= Time.deltaTime;
        }
        //Flip action buffering
        if (flipGravityAction.triggered) //If action was triggered this frame
        {
            Debug.Log("Flip action");
            flipBufferTimeCounter = flipBufferTime;
        }
        else
        {
            flipBufferTimeCounter -= Time.deltaTime;
        }
        
        //HORIZONTAL
        if(canAct) horizontalInput = moveAction.ReadValue<float>(); //read horizontal movement
        _velocity.x = horizontalInput * playerSpeed; //Set horizontal velocity

        if (horizontalInput != 0) lastHorizontalInput = horizontalInput; //Set last horizontal input

        //GRAVITY FLIP
        if (flipBufferTimeCounter > 0f && airFlipTimeCounter > 0f && canAct) //check if flip gravity triggered, and player is grounded
        {
            FlipGravity();
            airFlipTimeCounter = 0f;
            flipBufferTimeCounter = 0f;
        }
        
        //VERTICAL
        _velocity.y = rb.velocity.y; //keep y velocity consistent
        //Terminal velocity
        if (rb.velocity.y > terminalVelocity) _velocity.y = terminalVelocity;
        if (rb.velocity.y < -terminalVelocity) _velocity.y = -terminalVelocity;
        
        //Get target velocity
        Vector2 targetVelocity = new Vector2(horizontalInput * playerSpeed, _velocity.y);
        //Smooth damp to target velocity
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref _mVelocity, movementSmoothing);
        
        //ANIMATIONS
        playerAnimationController.AnimateMovement(horizontalInput);
    }

    //Can only flip gravity when grounded
    void FlipGravity()
    {
        Debug.Log("Player gravity flip");
        if (_isOnPlatform)
        {
            _isOnPlatform = false;
            platformRBody = null;
            StartCoroutine(BrieflyIgnorePlatformPhysics());
        }
        
        //reverse gravity direction
        desiredGravity.y = -desiredGravity.y;

        rb.gravityScale = desiredGravity.y / Physics2D.gravity.y; //Set rigidbody gravity scale instead
        
        //Provide small boost
        _velocity.y = desiredGravity.normalized.y * flipBoostStrength;
        
        //Do anims
        playerAnimationController.TriggerJumpAnim();
        
        //Invoke flip event
        onFlipGravityEvent.Invoke();
        
        //Play sound effect
        if(_playerSounds != null) _playerSounds.PlayFlipSound();
    }
    
    public void FlipGravity(bool isUp)
    {
        if((isUp && desiredGravity.y < 0) || (!isUp && desiredGravity.y > 0))
        {
            if (_isOnPlatform)
            {
                _isOnPlatform = false;
                platformRBody = null;
                StartCoroutine(BrieflyIgnorePlatformPhysics());
            }
            
            //reverse gravity direction
            desiredGravity.y = -desiredGravity.y;

            rb.gravityScale = desiredGravity.y / Physics2D.gravity.y; //Set rigidbody gravity scale instead

            //Provide small boost
            _velocity.y = desiredGravity.normalized.y * flipBoostStrength;

            //Do anims
            playerAnimationController.TriggerJumpAnim();
        }
    }

    //Check if player is grounded
    public bool IsGrounded()
    {
        var extraHeightTest = groundedHeightCheck; //Gives you a little bit of room to remain grounded
        var bounds = playerCollider.bounds;
        
        //Get ray direction
        var rayDirection = Vector2.zero;
        if (rb.gravityScale > 0) //CHANGED TO USE GRAVITY SCALE
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

    public void SetCanAct(bool state)
    {
        horizontalInput = 0;
        canAct = state;
    }

    public void PlayerWin()
    {
        SetCanAct(false);
        playerAnimationController.TriggerWinAnim();
        if(_playerSounds != null) _playerSounds.PlayWinSound();

        // try
        // {
        //     Destroy(FindObjectOfType<ControlIndicator>());
        // }
        // catch
        // {
        //     // ignored
        // }
    }

    
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (!_ignorePlatformPhysics && collision.gameObject.TryGetComponent(out AttachPlayerOnContact _))
    //     {
    //         platformRBody = collision.gameObject.GetComponent<Transform>();
    //         _lastPlatformPosition = platformRBody.position;
    //
    //         _isOnPlatform = true;
    //     }
    // }

    public void SetCurrentPlatform(Transform pTransform)
    {
        platformRBody = pTransform;
        _lastPlatformPosition = platformRBody.position;
    
        _isOnPlatform = true;
    }

    public void LeavePlatform()
    {
        _isOnPlatform = false;
        platformRBody = null;
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (!_ignorePlatformPhysics && collision.gameObject.TryGetComponent(out AttachPlayerOnContact _))
    //     {
    //         _platformRBody = collision.gameObject.GetComponent<Transform>();
    //         _lastPlatformPosition = _platformRBody.position;
    //
    //         _isOnPlatform = true;
    //     }
    // }
    //
    // private void OnCollisionExit(Collision collision)
    // {
    //     if (!_ignorePlatformPhysics && collision.gameObject.TryGetComponent(out AttachPlayerOnContact _))
    //     {
    //         _isOnPlatform = false;
    //         _platformRBody = null;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (!_ignorePlatformPhysics && collision.gameObject.TryGetComponent(out AttachPlayerOnContact _))
    //     {
    //         _isOnPlatform = false;
    //         platformRBody = null;
    //     }
    // }

    /**
     * Trigger to briefly ignore platform physics when flipping gravity
     */
    IEnumerator BrieflyIgnorePlatformPhysics()
    {
        // _ignorePlatformPhysics = true;
        // yield return new WaitForSeconds(0.1f);
        // _ignorePlatformPhysics = false;

        yield return null;
    }
}
