using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator MoveOverSpeed (Vector3 end, float speed){
        // speed should be 1 unit per second
        while (transform.position != end)
        {
            transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame ();
        }
        //End
        EndIndication();
    }
    
    public IEnumerator MoveOverSeconds (Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;
        
        //End
        EndIndication();
    }
    
    public IEnumerator MoveOverSeconds (Transform endTransform, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, endTransform.position, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = endTransform.position;
        
        //End
        EndIndication();
    }

    public void StartMoveOverSeconds(Transform endTransform, float seconds)
    {
        StartCoroutine(MoveOverSeconds(endTransform, seconds));
    }

    void EndIndication()
    {
        Destroy(gameObject);
    }
}
