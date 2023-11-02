using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObject : MonoBehaviour
{
    public int damage;

    // void OnCollisionEnter2D(Collision2D c)
    // {
    //     Health health = c.collider.GetComponent<Health>();
    //     if (health != null)
    //     {
    //         health.TakeDamage(damage);
    //     }
    // }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }
    }
}