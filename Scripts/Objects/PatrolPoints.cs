using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    // private Rigidbody2D rb;
    [SerializeField] private LineController _lineController;
    public List<Transform> patrolPointTransforms;
    private List<Vector3> patrolPoints = new List<Vector3>();

    public int objectivePointIndex;
    public int startPointIndex;

    [SerializeField] private Vector3 objectivePoint;

    public bool shouldPatrol = true;
    public bool shouldMove = true;
    public float speed = 5f;
    public float waitAtEachPoint = 1f;
    [SerializeField] private float thresholdDistance = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        //Draw indicator line
        if (_lineController != null)
        {
            _lineController.SetUpLine(patrolPointTransforms.ToArray());
        }
        
        //Use transforms list, in order
        if (patrolPointTransforms.Count != 0)
        {
            foreach (var pointTransform in patrolPointTransforms)
            {
                patrolPoints.Add(pointTransform.position);
            }
        } 
        // else if (patrolPoints.Count == 0)
        // {
        //     foreach (Transform child in transform)
        //     {
        //         patrolPoints.Add(child.position);
        //     }
        //     // patrolPoints = gameObject.GetComponentsInChildren<Transform>();
        // }

        objectivePointIndex = startPointIndex; //Set to start point
        transform.position = patrolPoints[startPointIndex];
        
        objectivePointIndex = NextPointIndex(); //Set objective to next point
        objectivePoint = patrolPoints[objectivePointIndex];

        // rb = GetComponent<Rigidbody2D>();

        StartCoroutine(PatrolAroundPoints());
    }

    // Update is called once per frame

    private int NextPointIndex()
    {
        int i = objectivePointIndex + 1;
        if (i >= patrolPoints.Count)
        {
            i = 0;
        }

        return i;
    }

    private IEnumerator PatrolAroundPoints()
    {
        while (shouldPatrol)
        {
            //If reached objective point
            if (Vector2.Distance(transform.position, objectivePoint) < thresholdDistance)
            {
                //Switch to next objective point
                objectivePointIndex = NextPointIndex();
                objectivePoint = patrolPoints[objectivePointIndex];
                
                //Wait for a bit
                yield return new WaitForSeconds(waitAtEachPoint);
            }
            
            
            //Move towards target point
            if (shouldMove)
            {
                var step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, objectivePoint, step);
                // rb.MovePosition(Vector2.MoveTowards(transform.position, objectivePoint, step));
            }

            yield return null; //Wait a frame
        }
        
    }
}
