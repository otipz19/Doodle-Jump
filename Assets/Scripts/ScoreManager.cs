using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static public ScoreManager S => s;
    static private ScoreManager s;

    public bool IsGameOver => isGameOver;
    private bool isGameOver;

    [SerializeField]
    private GameObject scorePanel;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text highScoreText;
    private int curScore;
    private int highScore;

    private void Awake()
    {
        if (s != null)
            throw new System.ApplicationException("ScoreManager.S is already asigned");
        s = this;
    }

    public void GameOver()
    {
        isGameOver = true;
        curScore = (int)Player.S.MaxY;
        Destroy(Player.S.gameObject);
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", curScore);
        highScore = PlayerPrefs.GetInt("HighScore");
        scorePanel.SetActive(true);
        scoreText.text = curScore.ToString();
        highScoreText.text = highScore.ToString();
    }
}
