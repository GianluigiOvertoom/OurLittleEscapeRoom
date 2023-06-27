using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScoring : MonoBehaviour
{
    public int Score = 0;
    public int MaxScore;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }
    public void AddScore(int NewScore)
    {
        Score += NewScore;
    }
}
