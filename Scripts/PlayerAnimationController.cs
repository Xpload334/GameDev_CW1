using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    
    [SerializeField] private PlayerController playerController;
    private Animator anim;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");
    private static readonly int JumpPar = Animator.StringToHash("Jump");
    private static readonly int GroundedPar = Animator.StringToHash("IsGrounded");

    private static readonly int FunnyFlipPar = Animator.StringToHash("FunnyFlip");
    [SerializeField] private Vector3 desiredRotation;
    [SerializeField] private Vector3 desiredLocalPosition = new Vector3(0, -1f, 0);
    [SerializeField] private float localPositionChange = 0.5f;
    [SerializeField] private float rotationSpeed = 5f;
    private float positionChangeSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        // playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        desiredRotation = new Vector3(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerController.horizontalInput)
        {
            //Animate walking
            case 0:
                anim.SetInteger(AnimationPar, 0);
                
                break;
            case > 0:
                desiredRotation.y = 90;
                anim.SetInteger(AnimationPar, 1);
                break;
            default:
                desiredRotation.y = -90;
                anim.SetInteger(AnimationPar, 1);
                break;
        }
        
        //Animate flipping
        if (playerController.gravityStrength > 0)
        {
            desiredRotation.z = 180;
            desiredLocalPosition.y = localPositionChange;
        }
        else
        {
            desiredRotation.z = 0;
            desiredLocalPosition.y = -localPositionChange;
        }
        
        //Animate falling
        anim.SetBool(GroundedPar, playerController.IsGrounded());

        //Handle position lerp
        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredLocalPosition, positionChangeSpeed * Time.deltaTime);
        //Handle rotation lerp
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(desiredRotation), rotationSpeed * Time.deltaTime);
    }

    public void TriggerJumpAnim()
    {
        anim.SetTrigger(JumpPar);
    }
    
    
}
