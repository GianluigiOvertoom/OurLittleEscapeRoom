using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringTrigger : MonoBehaviour
{
    public PointScoring Score;
    public LayerMask m_LayerMask;
    [SerializeField] private CircleCollider2D Circle;

    void Start()
    {
        Circle = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        MyCollisions();
    }

    // takes the size of the boxcollider, and checks if anything moves in contact with it, if so, checks if it is the correct object, and if so again, adds 1 to the score and removes the interacting game object
    private void MyCollisions()
    {
        // size of the gravity field
        Collider[] hitColliders = Physics.OverlapBox(Circle.transform.position, Circle.transform.localScale, Circle.transform.rotation, m_LayerMask);
        //Check when there is a new collider coming into contact with the box
        if (hitColliders.Length > 0)
        {
            foreach (Collider entry in hitColliders)
            {
                if (entry.GetComponent<ScoringControl>())
                {
                    Score.AddScore(1);
                }
            }
        }

    }

}
