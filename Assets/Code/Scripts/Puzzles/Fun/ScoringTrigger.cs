using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringTrigger : MonoBehaviour
{
    public LayerMask ballLayer;
    
    [SerializeField] private CircleCollider2D circle;
    [SerializeField] private GameObject keyPart;
    [SerializeField] private List<GameObject> basketBalls;
    [SerializeField] private ParticleSystem confetti;

    private List<GameObject> hitList;
    private bool puzzleFinished;

    void Start()
    {
        keyPart.SetActive(false);
        circle = GetComponent<CircleCollider2D>();
        hitList = new List<GameObject>();
        PointScoring.Score = basketBalls.Count;
        puzzleFinished = false;
    }

    void FixedUpdate()
    {
        if (puzzleFinished)
            return;
        MyCollisions();
    }


    private void MyCollisions()
    {
        // size of the gravity field
        Collider[] hitColliders = Physics.OverlapBox(circle.transform.position, circle.transform.localScale, circle.transform.rotation, ballLayer);
        //Check when there is a new collider coming into contact with the box
        if (hitColliders.Length > 0)
        {
            foreach (Collider entry in hitColliders)
            {
                if (entry.tag == "Key" || hitList.Contains(entry.gameObject))
                    return;
                hitList.Add(entry.gameObject);
                PointScoring.DepleteAmount();
                basketBalls.Remove(entry.gameObject);
                entry.gameObject.SetActive(false);
                hitList = new List<GameObject>();
                if (PointScoring.Score <= 0)
                {
                    FinishPuzzle();
                }
            }
        }

    }

    private void FinishPuzzle()
    {
        keyPart.SetActive(true);
        PointScoring.Score = 0;
        confetti.Play();
        puzzleFinished = true;
    }

}
