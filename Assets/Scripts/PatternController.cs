using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    [SerializeField] GameObject[] patterns;
    [SerializeField] GameObject potion;
    private GameObject              currentPattern;
    private int[]                   patternIndexes;
    private int current = 0;

    private void Awake()
    {
        patternIndexes = new int[patterns.Length];

        for(int i = 0; i< patternIndexes.Length; i++)
        {
            patternIndexes[i] = i;
        }
    }

    private void Update()
    {
        if (GameController.instance.IsGamePlay == false) return;

        if(currentPattern.activeSelf == false)
        {
            // 다음 패턴 실행
            ChangePattern();
        }
    }

    public void GameStart()
    {
        ChangePattern();
    }

    public void GameOver()
    {
        currentPattern.SetActive(false);
    }

    public void ChangePattern()
    {
        currentPattern = patterns[patternIndexes[current]];

        currentPattern.SetActive(true);

        current++;

        if(current == 4 || current == 8)
        {
            potion.SetActive(true);
        }

        if( current >= patternIndexes.Length)
        {
            patternIndexes = Utils.RandomNumbers(patternIndexes.Length, patternIndexes.Length);
            current = 0;
        }
    }
}
