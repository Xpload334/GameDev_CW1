using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private List<RequiresCollectingKeys> objectsToActivate;
    public UnityEvent onCollect;
    private Collider2D _collider2D;
    private bool _isCollected;

    [SerializeField] private KeyIndicator keyIndicator;
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
            TriggerKeyEffect();
        }
    }

    /**
     * Send a key image that goes towards all the things it activates, then disappears
     */
    private void TriggerKeyEffect()
    {
        _isCollected = true;
        onCollect?.Invoke();
        foreach (var keyObject in objectsToActivate)
        {
            keyObject.IncrementCount();
            KeyIndicator iKeyIndicator = Instantiate(keyIndicator, transform.position, transform.rotation);
            iKeyIndicator.StartMoveOverSeconds(keyObject.transform, 0.5f);
        }
        
        gameObject.SetActive(false);
    }
}
