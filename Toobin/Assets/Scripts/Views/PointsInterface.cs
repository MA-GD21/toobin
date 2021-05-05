using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsInterface : MonoBehaviour
{
    [SerializeField] TMP_Text m_lives;
    [SerializeField] TMP_Text m_points;

    public void UpdatePoints(int value)
    {
        m_points.text = "Points: " + value;
    }

    public void UpdateLives(int value)
    {
        m_lives.text = "Lives: " + value;
    }

    public void Button_OnReset()
    {
        SceneManager.LoadScene("Main");
    }
}
