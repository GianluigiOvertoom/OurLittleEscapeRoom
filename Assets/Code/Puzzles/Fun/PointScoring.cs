using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointScoring : MonoBehaviour
{
    public Text ScoreText;
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

    public void UpdatScore()
    {
        ScoreText.text = "Balls collected: " + Score;
    }
    // Update is called once per frame
    void Update()
    {
        UpdatScore();
    }
}
