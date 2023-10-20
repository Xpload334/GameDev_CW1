using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class ExitDoor : MonoBehaviour
{
    public bool isOpen;
    public bool isLevelComplete;
    public PlayerController playerAtDoor;
    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        StartCoroutine(DoorCheckIEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoorCheckIEnumerator()
    {
        while (!CanExit())
        {
            yield return new WaitForSeconds(0.1f);
        } 
        
        //Trigger exit door condition
        isLevelComplete = true;
        Debug.Log("Level Complete!");
    }

    bool CanExit()
    {
        if (!isOpen) return false;
        if (playerAtDoor == null) return false;
        if (!playerAtDoor.IsGrounded()) return false;


        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player) && !isLevelComplete)
        {
            this.playerAtDoor = player;
            Debug.Log(player.name+" is at the door");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            playerAtDoor = null;
        }
    }
}
