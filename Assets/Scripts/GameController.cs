using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private UIController uiController;

    [SerializeField] PatternController patternController;
    //[SerializeField] private GameObject pattern01;

    [SerializeField] PlayerController playerController;

    private readonly float scoreScale = 20;

    public bool IsGamePlay { private set; get; } = false;

    public float CurrentScore { private set; get; } = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CurrentScore += Time.deltaTime * scoreScale;
    }

    public void GameStart()
    {
        uiController.GameStart();

        playerController.Reset();

        //pattern01.SetActive(true);
        patternController.GameStart();

        IsGamePlay = true;
    }

    public void GameExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();

    #else
    Application.Quit();
    #endif
    }

    public void GameOver()
    {
        uiController.GameOver();

        //pattern01.SetActive(false);
        patternController.GameOver();

        IsGamePlay = false;
    }
}
