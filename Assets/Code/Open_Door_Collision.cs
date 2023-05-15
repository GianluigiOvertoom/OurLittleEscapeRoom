using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Open_Door_Collision : MonoBehaviour
{
    // Creates variables that are not editable outside of this file, except for the animator
    public Animator animator;
    [SerializeField] private Collider Door_plate;
    [SerializeField] private int keysPresent = 0;
    private string keyTag = "Key";
    private string A_openDoor = "Door_open";

    void Start() 
    {
        // checks how many objects are interacting with the plate
        Door_plate = GetComponent<Collider>();
    }
    // checks to see if enough objects with the tag "key" have been collected
    void OnTriggerEnter(Collider other) 
    { 
        if (other.transform.CompareTag(keyTag)) 
        {
            keysPresent++;
            if (keysPresent >= 7) OpenDoor();
        }
    }
    // calls the animation to open the door
    void OpenDoor() 
    { 
        animator.SetBool(A_openDoor, true);
    }
    // checks to see if any "keys" have been removed
    void OnTriggerExit(Collider other)
    { 
        if (other.transform.CompareTag(keyTag))
        {
            keysPresent--;
            if (keysPresent < 7) CloseDoor();
        }
    }
    // calls the animation to close the door
    void CloseDoor() 
    {
        animator.SetBool(A_openDoor, false);
    }
}