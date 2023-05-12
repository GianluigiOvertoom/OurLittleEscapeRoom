using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityControl : MonoBehaviour
{
    // links to rigidbodies to use the gravity
    public gravityOrbit Gravity;
    private Rigidbody Rb;

    public float rotationSpeed = 20;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // the objects reaction to gravity
    void Update()
    {
        if (Gravity)
        {
            Vector3 gravityUp = Vector3.zero;
            gravityUp = (tranform.position - Gravity.transform.position).normalized;
            Vector3 localUp = transform.up;
            Quaternion targetrotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
            transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);
            // force
            Rb.AddForce((-gravityUp * Gravity.Gravity) * Rb.mass);
        }
    }
}
