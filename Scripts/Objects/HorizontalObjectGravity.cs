using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObjectGravity : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask groundLayerMask; // What layer to use for ground tiles
    public float horizontalGravity = -50f; // Strength of horizontal gravity
    [SerializeField] private float flipBoostStrength = 5f; // Boost in velocity to give when flipping
    [SerializeField] private float terminalVelocity = 20f; // Max velocity
    public float playerSpeed = 7.0f;
    [SerializeField] private float movementSmoothing = 0.05f;
    private Vector2 m_Velocity;
    private Vector2 _velocity;
    public bool invert;
    public bool flipEnabled;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (invert) horizontalGravity = -horizontalGravity;

        // Apply horizontal gravity manually
        Vector2 horizontalGravityForce = new Vector2(horizontalGravity, 0);
        _rb.AddForce(horizontalGravityForce);
    }


    public void FlipGravity()
    {
	if(flipEnabled)
	{
        	// Reverse gravity direction
        	horizontalGravity = -horizontalGravity;
        	_rb.velocity = new Vector2(0,0);
        	// Apply horizontal gravity manually
        	Vector2 horizontalGravityForce = new Vector2(horizontalGravity, 0);
        	_rb.AddForce(horizontalGravityForce);
	}
    }

    public void FlipEnable()
    {
	flipEnabled = true;
    }

}
