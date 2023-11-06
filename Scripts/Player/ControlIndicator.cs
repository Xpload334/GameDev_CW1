using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlIndicator : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private InputAction moveAction; //Action for moving player horizontally
    private InputAction flipGravityAction; //Action for flipping gravity
    private bool gravityFlipPress;
    private float horizontalInput;

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
        moveAction = _playerController.moveAction;
        flipGravityAction = _playerController.flipGravityAction;
        
        flipGravityAction.started += _ =>
        {
            
        };

        flipGravityAction.canceled += _ =>
        {

        };
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = moveAction.ReadValue<float>();

        if (horizontalInput < 0)
        {
            LeftKeyPressed();
            RightKeyReleased();
        }
        else if(horizontalInput > 0)
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
        keyFlipImage.sprite = keySpacePressed;
    }

    void GravityActionReleased()
    {
        keyFlipImage.sprite = keySpace;
    }

    void LeftKeyPressed()
    {
        keyLeftImage.sprite = keyLeftPressed;
    }

    void LeftKeyReleased()
    {
        keyLeftImage.sprite = keyLeft;
    }
    
    void RightKeyPressed()
    {
        keyRightImage.sprite = keyRightPressed;
    }

    void RightKeyReleased()
    {
        keyRightImage.sprite = keyRight;
    }
    
    
}
