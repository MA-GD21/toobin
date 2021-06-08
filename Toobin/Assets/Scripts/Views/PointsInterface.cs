
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsInterface : MonoBehaviour
{
    [SerializeField] TMP_Text m_lives;
    [SerializeField] TMP_Text m_points;
    [SerializeField] TMP_Text m_lives_p2;
    [SerializeField] TMP_Text m_points_p2;
    [SerializeField] TMP_Text highscore;
    int m_pointValue;
    int m_livesvalue;
    int m_highscore;
    
    int m_pointValue_p2;
    int m_livesvalue_p2;
    
    void Start()
    {
        m_highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscore.text = "highscore: " + m_highscore.ToString();
    }

    public void UpdatePoints(int value, int playerID)
    {
        if (playerID == 0)
        {
            m_pointValue = value;
            m_points.text = "Points: " + value;
        }
        else
        {
            m_pointValue_p2 = value;
            m_points_p2.text = "Points: " + value;
        }
    }

    public void UpdateLives(int value, int playerID)
    {
        if (playerID == 0)
        {
            m_livesvalue = value;
            m_lives.text = "Lives: " + value;
        }
        else
        {
            m_livesvalue_p2 = value;
            m_lives_p2.text = "Lives: " + value;
        }
    }

    //public void Button_OnReset();
 
    public void Button_OnReset()
    {
        SceneManager.LoadScene("Main");
    }

    public void UpdateHighscore()
    {
        int currentScore = Math.Max(m_pointValue, m_pointValue_p2);
        if (currentScore > m_highscore)
        {
            PlayerPrefs.SetInt("Highscore", currentScore);
            m_highscore = currentScore;
            highscore.text = "highscore: " + m_highscore.ToString();
        }
    }

}