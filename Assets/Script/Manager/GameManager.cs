using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score;
    public List<HighScoreData> highScores = new List<HighScoreData>();

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int scoreAward)
    {
        score += scoreAward;
        
    }



    public void NextLevel(int level)
    {
        SceneManager.LoadScene("Level "+level);
    }
}