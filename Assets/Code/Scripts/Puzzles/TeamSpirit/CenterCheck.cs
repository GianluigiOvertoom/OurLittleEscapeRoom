using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCheck : MonoBehaviour
{
    [SerializeField] private ParticleSystem confetti;
    private bool puzzleFinished;
    private void Start()
    {
        puzzleFinished = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !puzzleFinished)
        {
            Debug.Log("!");
            confetti.Play();
            puzzleFinished = true;
        }
    }
}
