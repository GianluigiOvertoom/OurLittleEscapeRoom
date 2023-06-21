using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    public float Gravity;
    public bool FixedDirection;
    public LayerMask m_LayerMask;
    [SerializeField] private BoxCollider Box;
    [SerializeField] private GameObject Reference;

    void Start()
    {
        Box = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        MyCollisions();
    }


    private void MyCollisions()
    {
        // size of the gravity field
        Collider[] hitColliders = Physics.OverlapBox(Box.transform.position, Reference.transform.localScale, Reference.transform.rotation, m_LayerMask);
        //Check when there is a new collider coming into contact with the box
        if (hitColliders.Length > 0)
        {
            foreach(Collider entry in hitColliders)
            {
                if (entry.GetComponent<GravityControl>())
                {
                    //checks if the object interacting has a gravity script, if so enable the current gravity for that object
                    entry.GetComponent<GravityControl>().Gravity = this.GetComponent<GravityOrbit>();
                }
            }
        }

    }

}