using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringTrigger : MonoBehaviour
{
    public PointScoring Score;
    public LayerMask m_LayerMask;
    [SerializeField] private BoxCollider Box;

    void Start()
    {
        Box = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        MyCollisions();
    }

    // takes the size of the boxcollider, and checks if anything moves in contact with it, if so, checks if it is the correct object, and if so again, adds 1 to the score and removes the interacting game object
    private void MyCollisions()
    {
        Collider[] hitColliders = Physics.OverlapBox(Box.transform.position, Box.transform.localScale, Box.transform.rotation, m_LayerMask);
        if (hitColliders.Length > 0)
        {
            foreach (Collider entry in hitColliders)
            {
                if (entry.GetComponent<ScoringControl>())
                {
                    Score.AddScore(1);
                    entry.transform.gameObject.SetActive(false);
                }
            }
        }

    }

}
