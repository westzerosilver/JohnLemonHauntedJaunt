using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOption;

    GameState m_GameState;

    public enum GameState
    {
        Run,
        Pause,
        GameOver,
    }
    // Start is called before the first frame update
    void Start()
    {
        m_GameState = GameState.Run;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOption()
    {
        gameOption.SetActive(true);
        Time.timeScale = 0f;
        m_GameState = GameState.Pause;
    }

    public void CloseOption()
    {
        gameOption.SetActive(false);
        Time.timeScale = 1f;
        m_GameState = GameState.Run;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
