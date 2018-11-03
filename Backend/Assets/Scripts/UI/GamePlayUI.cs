using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour, IScreen
{
    [SerializeField]
    private Text mCurrentScoreText;

    public void INIT()
    {
        ShowCurrentScore();
        CupManager.Instance.StartGamePlay();
    }

    public void Update()
    {
        ShowCurrentScore();
    }

    void ShowCurrentScore()
    {
        mCurrentScoreText.text = "Points: " + CupManager.Instance.score.MScore;
    }
}
