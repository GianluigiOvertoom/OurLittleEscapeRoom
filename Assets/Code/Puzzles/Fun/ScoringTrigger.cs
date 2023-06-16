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
