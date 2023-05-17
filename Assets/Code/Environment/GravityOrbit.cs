using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    bool m_Started;
    public float Gravity;
    public bool FixedDirection;
    public LayerMask m_LayerMask;
    [SerializeField] private BoxCollider Box;
    [SerializeField] private GameObject Reference;

    void Start()
    {
        m_Started = true;
        Box = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        MyCollisions();
    }


    private void MyCollisions()
    {
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
    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
       if (m_Started)
        {
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(Box.transform.position, Reference.transform.localScale);
        }

    }

}
//new Quaternion(Reference.transform.rotation.x, Reference.transform.rotation.y, Reference.transform.rotation.z, Reference.transform.rotation.w)
//Reference.transform.rotation