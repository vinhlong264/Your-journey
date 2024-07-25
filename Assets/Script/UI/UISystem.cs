using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }

    void updateUI()
    {
        scoreText.text = GameManager.Instance.score.ToString();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Menu");
    }
}
