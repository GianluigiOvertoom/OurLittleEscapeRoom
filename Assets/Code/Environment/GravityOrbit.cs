using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    public float Gravity;
    public bool FixedDirection;

    private void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
    }

    private void OntriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.GetComponent<GravityControl>())
        {
            //checks if the object interacting has a gravity script, if so enable the current gravity for that object
            other.GetComponent<GravityControl>().Gravity = this.GetComponent<GravityOrbit>();
        }
    }

    private void OnTrigger(Collider other)
    {
        print(other.gameObject.name);
    }
}
