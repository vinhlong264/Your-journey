using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveUI : MonoBehaviour
{
    public GameObject highScore;
    public GameObject YouWin;

    public void activeScore()
    {
        highScore.SetActive(true);
        YouWin.SetActive(false);
    }

    public void activeMenu()
    {
        highScore.SetActive(false);
        YouWin.SetActive(true);
    }

    public void playGame()
    {
        GameManager.Instance.NextLevel(1);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
