using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ballcounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _title;
    
    public int BallAmount;
    public int BallCheck;

    // Start is called before the first frame update
    void Start()
    {
        _title.text = (BallAmount.ToString());
    }

    private void Update()
    {
        BallAmount = PointScoring.Score;

        if (BallAmount != BallCheck)
        {
            _title.text = ("" + BallAmount);
            BallCheck = BallAmount;
        }
    }
}
