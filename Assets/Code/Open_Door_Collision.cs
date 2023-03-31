using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Open_Door_Collision : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Collider Door_plate;
    [SerializeField] private int Keys_present = 0;
    private string Key_tag = "Key";
    private string A_OpenDoor = "Door_open";

    void Start() {
        Door_plate = GetComponent<Collider>();
    }
    void OnTriggerEnter(Collider other) { // copy paste for door close, use: OnTriggerExit
        if (other.transform.CompareTag(Key_tag)) {
            Keys_present++;
            if (Keys_present >= 7) OpenDoor();
        }
    }
    void OpenDoor() { // copy paste for door close, use: void closeDoor
        animator.SetBool(A_OpenDoor, true);
    }
}