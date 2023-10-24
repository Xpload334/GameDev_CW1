using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalObjectGravity : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask groundLayerMask; //What layer to use for ground tiles
    public Vector2 desiredGravity = new(0, -50f); //Vector for gravity
    [SerializeField] private float flipBoostStrength = 5f; //Boost in velocity to give when flipping
    [SerializeField] private float terminalVelocity = 20f; //Max velocity
    public float playerSpeed = 7.0f;
    [SerializeField] private float movementSmoothing = .05f;
    private Vector2 m_Velocity;
    private Vector2 _velocity;
    public bool invert;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (invert) desiredGravity.y = -desiredGravity.y;
        _rb.gravityScale = desiredGravity.y / Physics2D.gravity.y;
        _velocity = Vector3.zero;
    }

    //Can only flip gravity when grounded
    public void FlipGravity()
    {
        //reverse gravity direction
        desiredGravity.y = -desiredGravity.y;
        // Physics2D.gravity = new Vector2(Physics2D.gravity.x, desiredGravity.y);
        _rb.gravityScale = desiredGravity.y / Physics2D.gravity.y; //Set rigidbody gravity scale instead

        //Provide small boost
        _velocity.y = desiredGravity.normalized.y * flipBoostStrength;
    }
}
