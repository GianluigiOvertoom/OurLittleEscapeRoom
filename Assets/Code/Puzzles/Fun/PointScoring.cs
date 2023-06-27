using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointScoring : MonoBehaviour
{
    public Text ScoreText;
    public int Score = 0;
    public int MaxScore;

    // reset the score
    void Start()
    {
        Score = 0;
    }

    // so points can be added to the score
    public void AddScore(int NewScore)
    {
        Score += NewScore;
    }

    // sets up the text for the ui
    public void UpdatScore()
    {
        ScoreText.text = "Balls collected: " + Score;
    }
    // Update the score
    void Update()
    {
        UpdatScore();
    }
}
