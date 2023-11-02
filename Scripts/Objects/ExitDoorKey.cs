using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitDoorKey : MonoBehaviour
{
    public UnityEvent onCollect;
    private Collider2D _collider2D;
    private bool _isCollected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController _) && !_isCollected)
        {
            Debug.Log(name+" collected");
            _isCollected = true;
            onCollect.Invoke();
            gameObject.SetActive(false);
        }
    }
}
