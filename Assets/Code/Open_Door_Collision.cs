using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Open_Door_Collision : MonoBehaviour
{
    // Creates variables that are not editable outside of this file, except for the animator
    public Animator animator;
    [SerializeField] private Collider Door_plate;
    [SerializeField] private int Keys_present = 0;
    private string Key_tag = "Key";
    private string A_OpenDoor = "Door_open";

    void Start() {
        Door_plate = GetComponent<Collider>();
    }
    // checks to see if enough keys have been collected
    void OnTriggerEnter(Collider other) { 
        if (other.transform.CompareTag(Key_tag)) {
            Keys_present++;
            if (Keys_present >= 7) OpenDoor();
        }
    }
    // calls the animation to open the door
    void OpenDoor() { 
        animator.SetBool(A_OpenDoor, true);
        Debug.Log(A_OpenDoor);
    }
    // checks to see if any keys have been removed
    void OnTriggerExit(Collider other) { 
        if (other.transform.CompareTag(Key_tag))
        {
            Keys_present--;
            if (Keys_present < 7) CloseDoor();
        }
    }
    // calls the animation to close the door
    void CloseDoor() {
        animator.SetBool(A_OpenDoor, false);
    }
}