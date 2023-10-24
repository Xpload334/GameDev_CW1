using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    public List<Vector3> patrolPoints;

    public int objectivePointIndex;
    public int startPointIndex = 0;

    [SerializeField] private Vector3 objectivePoint;

    public bool ShouldPatrol = true;
    public bool ShouldMove = true;
    public float speed = 5f;
    public float waitAtEachPoint = 1f;
    [SerializeField] private float thresholdDistance = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        if (patrolPoints.Count == 0)
        {
            foreach (Transform child in transform)
            {
                patrolPoints.Add(child.position);
            }
            // patrolPoints = gameObject.GetComponentsInChildren<Transform>();
        }

        objectivePointIndex = startPointIndex; //Set to start point
        transform.position = patrolPoints[startPointIndex];
        
        objectivePointIndex = NextPointIndex(); //Set objective to next point
        objectivePoint = patrolPoints[objectivePointIndex];
        
        
        StartCoroutine(PatrolAroundPoints());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int NextPointIndex()
    {
        int i = objectivePointIndex + 1;
        if (i >= patrolPoints.Count)
        {
            i = 0;
        }

        return i;
    }

    IEnumerator PatrolAroundPoints()
    {
        while (ShouldPatrol)
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
            if (ShouldMove)
            {
                var step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, objectivePoint, step);
            }

            yield return null; //Wait a frame
        }
        
    }
}
