using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlIndicator : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private InputAction _moveAction; //Action for moving player horizontally
    private InputAction _flipGravityAction; //Action for flipping gravity
    private bool _gravityFlipPress;
    private float _horizontalInput;

    public SpriteRenderer keyLeftImage;
    public SpriteRenderer keyRightImage;
    public SpriteRenderer keyFlipImage;
    
    public Sprite keyLeft;
    public Sprite keyLeftPressed;
    public Sprite keyRight;
    public Sprite keyRightPressed;
    public Sprite keySpace;
    public Sprite keySpacePressed;
    // public Sprite keyA;
    // public Sprite keyAPressed;
    // public Sprite keyD;
    // public Sprite keyDPressed;
    // public Sprite keyQ;
    // public Sprite keyQPressed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
        _moveAction = playerController.moveAction;
        _flipGravityAction = playerController.flipGravityAction;
        
        _flipGravityAction.started += _ =>
        {
            GravityActionPressed();
        };

        _flipGravityAction.canceled += _ =>
        {
            GravityActionReleased();
        };
    }
    

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = _moveAction.ReadValue<float>();

        if (_horizontalInput < 0)
        {
            LeftKeyPressed();
            RightKeyReleased();
        }
        else if(_horizontalInput > 0)
        {
            RightKeyPressed();
            LeftKeyReleased();
        }
        else
        {
            LeftKeyReleased();
            RightKeyReleased();
        }
    }

    void GravityActionPressed()
    {
        if (keyFlipImage != null)
        {
            keyFlipImage.sprite = keySpacePressed;
        }
    }

    void GravityActionReleased()
    {
        if (keyFlipImage != null)
        {
            keyFlipImage.sprite = keySpace;
        }
    }

    void LeftKeyPressed()
    {
        if (keyLeftImage != null)
        {
            keyLeftImage.sprite = keyLeftPressed;
        }
    }

    void LeftKeyReleased()
    {
        if (keyLeftImage != null)
        {
            keyLeftImage.sprite = keyLeft;
        }
    }

    void RightKeyPressed()
    {
        if (keyRightImage != null)
        {
            keyRightImage.sprite = keyRightPressed;
        }
    }

    void RightKeyReleased()
    {
        if (keyRightImage != null)
        {
            keyRightImage.sprite = keyRight;
        }
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from input actions
        if (_flipGravityAction != null)
        {
            _flipGravityAction.started -= _ => { GravityActionPressed(); };
            _flipGravityAction.canceled -= _ => { GravityActionReleased(); };
        }
    }
    
    
}
