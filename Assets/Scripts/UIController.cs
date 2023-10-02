using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [Header("Main UI")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TextMeshProUGUI textMainGrade;

    [Header("Game UI")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TextMeshProUGUI textScore;

    [Header("Result UI")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] TextMeshProUGUI textResultScore;
    [SerializeField] TextMeshProUGUI textResultGrade;
    [SerializeField] TextMeshProUGUI textResultTalk;
    [SerializeField] TextMeshProUGUI textResultHightScore;

    private void Awake()
    {
        textMainGrade.text = PlayerPrefs.GetString("HIGHGRADE");
    }

    private void Update()
    {
        textScore.text = gameController.CurrentScore.ToString("F0");
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void GameOver()
    {
        int currentScore = (int)gameController.CurrentScore;

        textResultScore.text = currentScore.ToString();

        CalculateGradeAndTalk(currentScore);
        CalculateHighScore(currentScore);

        gamePanel.SetActive(false);
        resultPanel.SetActive(true);
    }

    public void MainLobby()
    {
        SceneManager.LoadScene(0);
    }

    private void CalculateGradeAndTalk(int score)
    {
        if(score < 2000)
        {
            textResultGrade.text = "F";
            textResultTalk.text = "ÂÍ ´õ ³ë·ÂÇØº¾½Ã´Ù!";
        }
    }

    private void CalculateHighScore(int score)
    {
        int hightScore = PlayerPrefs.GetInt("HIGHSCORE");

        if( score > hightScore)
        {
            PlayerPrefs.SetString("HIGHGRADE", textResultGrade.text);

            PlayerPrefs.SetInt("HIGHSCORE", score);

            textResultHightScore.text = score.ToString();
        }
        else
        {
            textResultHightScore.text = hightScore.ToString();
        }
    }
}
