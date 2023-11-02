using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        ResetPlayerBuff();
        score.text ="Your score:" + scoreKeeper.GetScore().ToString();
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetFloat("Money", 0);
        }
        float temp = PlayerPrefs.GetFloat("Money");
        temp += scoreKeeper.GetScore();
        PlayerPrefs.SetFloat("Money", temp);
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", scoreKeeper.GetScore());
        }
        else
        {
            float t = PlayerPrefs.GetFloat("HighScore");
            if (scoreKeeper.GetScore() > t)
            {
                PlayerPrefs.SetFloat("HighScore", scoreKeeper.GetScore());
            }
        }
    
    }
    private void ResetPlayerBuff()
    {
        if(PlayerPrefs.HasKey("PlayerHP") && PlayerPrefs.HasKey("PlayerBulletSpeed"))
        {
            PlayerPrefs.SetFloat("PlayerHP", 50f);
            PlayerPrefs.SetFloat("PlayerBulletSpeed", 10f);
        }
    }

    void Update()
    {
        
    }
}
