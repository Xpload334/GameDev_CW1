using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGrav : MonoBehaviour
{
    public bool isUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.GetComponent<PlayerController>().FlipGravity(isUp);
        }
    }
}
