using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    public List<HighScoreData> highScores = new List<HighScoreData>();
    public GameObject scoreLine;
    public Transform content;
    private int lastCheckedScore = -1;

    void Start()
    {
        LoadHighScores();
        UpdateHighScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        int currentScore = GameManager.Instance.score;

        if (currentScore != lastCheckedScore)
        {
            lastCheckedScore = currentScore;
            AddNewHighScore(currentScore);
        }
    }

    public void AddNewHighScore(int newScore)
    {
        HighScoreData context = new HighScoreData();
        context.score = newScore;

        if (highScores.Count < 3)
        {
            highScores.Add(context);
        }
        else
        {
            var getMin = highScores.OrderBy(x => x.score).FirstOrDefault();
            if (newScore > getMin.score)
            {
                highScores.Remove(getMin);
                highScores.Add(context);
            }
        }

        highScores = highScores.OrderByDescending(x => x.score).ToList();
        SaveHighScores();
        UpdateHighScoreUI();
    }

    private void UpdateHighScoreUI()
    {
        foreach (Transform line in content)
        {
            Destroy(line.gameObject);
        }

        for (int i = 0; i < highScores.Count; i++)
        {
            highScores[i].rank = i + 1;
            GameObject line = Instantiate(scoreLine, content);
            line.GetComponent<ScoreLineController>().SetText(highScores[i]);
        }
    }

    private void SaveHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i + "Rank", highScores[i].rank);
            PlayerPrefs.SetInt("HighScore" + i + "Score", highScores[i].score);
        }
        PlayerPrefs.SetInt("HighScoreCount", highScores.Count);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        highScores.Clear();
        int count = PlayerPrefs.GetInt("HighScoreCount", 0);

        for (int i = 0; i < count; i++)
        {
            HighScoreData data = new HighScoreData();
            data.rank = PlayerPrefs.GetInt("HighScore" + i + "Rank");
            data.score = PlayerPrefs.GetInt("HighScore" + i + "Score");
            highScores.Add(data);
        }

        highScores = highScores.OrderByDescending(x => x.score).ToList();
    }
}

[Serializable]
public class HighScoreData
{
    public int rank;
    public int score;
}