using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbSteps : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private GameObject stepRayUpper;

    [SerializeField] private GameObject stepRayLower;

    [SerializeField] private float stepHeight = 0.3f;

    [SerializeField] private float stepSmooth;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        
        var position = stepRayUpper.transform.position;
        position = new Vector3(position.x, stepHeight, position.z);
        stepRayUpper.transform.position = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StepClimb();
    }


    void StepClimb()
    {
        Vector2 lowerRayPos = new Vector2(stepRayLower.transform.position.x, stepRayLower.transform.position.y);
        RaycastHit2D hitLower = Physics2D.Raycast(lowerRayPos, new Vector2(_playerController.lastHorizontalInput, 0), 0.1f,
            _playerController.groundLayerMask);
        if (hitLower)
        {
            Vector2 upperRayPos = new Vector2(stepRayUpper.transform.position.x, stepRayUpper.transform.position.y);
            RaycastHit2D hitUpper = Physics2D.Raycast(upperRayPos, new Vector2(_playerController.lastHorizontalInput, 0), 0.2f,
                _playerController.groundLayerMask);
            if (!hitUpper)
            {
                _playerController.rb.position -= new Vector2(0f, -stepSmooth);
            }
            
        }

    }
}
