using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { StartScreen, Level1, GameOver }
public class GameManager : MonoBehaviour
{
    private static GameManager m_instance = null;
    [SerializeField] GameState m_gameState = GameState.StartScreen;
    [SerializeField] GameObject m_level;
    [SerializeField] GameObject m_startScreen;
    [SerializeField] GameObject m_gameOver;
    [SerializeField] LevelLoader m_levelLoader;
    [SerializeField] Camera mainCamera;
    [SerializeField] PointsInterface m_pointsInterface;

    int m_points;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        SetGameState(GameState.StartScreen);
    }


    public GameState GameState { get => m_gameState; set => m_gameState = value; }
    public int Points { get => m_points; set => m_points = value; }
    public PointsInterface PointsInterface { get => m_pointsInterface; set => m_pointsInterface = value; }

    public void SetGameState(GameState gameState)
    {
        this.m_gameState = gameState;
        switch (gameState)
        {
            case GameState.StartScreen:
                m_level.SetActive(false);
                m_startScreen.SetActive(true);
                m_gameOver.SetActive(false);
                //play sound
                FindObjectOfType<AudioManager>().Play("ToobinTheme");
                break;

            case GameState.Level1:
                m_levelLoader.PlayAnimation();
                m_startScreen.SetActive(false);
                m_level.SetActive(true);
                m_gameOver.SetActive(false);
                //play sound
                FindObjectOfType<AudioManager>().Stop("ToobinTheme");
                FindObjectOfType<AudioManager>().Play("FirstStageTheme");
                break;

            case GameState.GameOver:
                //m_startScreen.SetActive(false);
                //m_level.SetActive(false);
                m_gameOver.SetActive(true);
                FindObjectOfType<AudioManager>().Stop("FirstStageTheme");
                break;
        }
    }

    public void StartLevel1()
    {
        SetGameState(GameState.Level1);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        SetGameState(GameState.GameOver);
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Main");
    }

    public void SetCameraAxis(float playerXPosition, float playerYPosition)
    {
        mainCamera.transform.position = new Vector3(playerXPosition, playerYPosition, 0);
    }
}