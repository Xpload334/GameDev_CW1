using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RequiresCollectingKeys : MonoBehaviour
{
    public UnityEvent onKeysCollected; //Event to trigger when the number of keys has been activate
    public int countToTriggerEvent; //Number of keys to activate the event
    [SerializeField] private int currentKeysCollected = 0; //current key count
    private bool _isCompleted;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementCount()
    {
        currentKeysCollected++;
        
        //If keys collected
        if (currentKeysCollected >= countToTriggerEvent && !_isCompleted)
        {
            Debug.Log(currentKeysCollected+" keys collected, event triggered");
            _isCompleted = true;
            onKeysCollected.Invoke();
        }
    }
}
