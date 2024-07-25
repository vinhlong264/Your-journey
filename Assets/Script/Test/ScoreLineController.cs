using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLineController : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI scoreText;

    public void SetText(HighScoreData data)
    {
        rankText.text = data.rank.ToString();
        scoreText.text = data.score.ToString();
    }


}
