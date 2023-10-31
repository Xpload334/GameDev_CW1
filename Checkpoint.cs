using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent enableEvent;
	
    private void OnTriggerEnter2D(Collider2D other)
    {
	if (other.TryGetComponent(out PlayerController player))
        {
        	Debug.Log("Flip enabled");
        	enableEvent.Invoke();
	}
    }
}
