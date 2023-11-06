using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttachPlayerOnContact : MonoBehaviour
{
    // private void OnCollisionEnter2D(Collision other)
    // {
    //     if (other.gameObject.TryGetComponent(out PlayerController player))
    //     {
    //         player.SetCurrentPlatform(this.transform);
    //     }
    // }
    

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.TryGetComponent(out PlayerController player))
    //     {
    //         if (player.platformRBody == transform)
    //         {
    //             player.LeavePlatform();
    //         }
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.SetCurrentPlatform(this.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            if (player.platformRBody == transform)
            {
                player.LeavePlatform();
            }
        }
    }
    
    

}
