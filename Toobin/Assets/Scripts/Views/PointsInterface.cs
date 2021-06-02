
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsInterface : MonoBehaviour
{
    [SerializeField] TMP_Text m_lives;
    [SerializeField] TMP_Text m_points;
    [SerializeField] TMP_Text highscore;
    int m_pointValue;
    int m_livesvalue;
    int m_highscore;

    void Start()
    {
        m_highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscore.text = "highscore: " + m_highscore.ToString();
    }

    public void UpdatePoints(int value)
    {
        m_pointValue = value;
        m_points.text = "Points: " + value;
    }

    public void UpdateLives(int value)
    {
        m_livesvalue = value;
        m_lives.text = "Lives: " + value;
    }

    //public void Button_OnReset();
 
public void Button_OnReset()
    {
        SceneManager.LoadScene("Main");
    }

    public void UpdateHighscore(int value)
    {
        if (value > m_highscore)
        {
            PlayerPrefs.SetInt("Highscore", value);
            m_highscore = value;
            highscore.text = "highscore: " + m_highscore.ToString();
        }
    }

}