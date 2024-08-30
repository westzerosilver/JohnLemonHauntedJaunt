using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.UI;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject gameOption;
    public Text StopWatchText;
    public Text BestTimeText;

    GameState m_GameState;
    string m_FilePath;
    bool m_HasSaved;
    Stopwatch watch;
    

    public enum GameState
    {
        Run,
        Pause,
        GameOver,
        GameClear,
    }

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_GameState = GameState.Run;
        m_FilePath = "/StopwatchTimeRecord.txt";
        m_HasSaved = false;

        watch = new Stopwatch();
        watch.Start();

        // 최고 기록 가져오기
        TimeSpan bestTime = TimeSpan.MaxValue;
        string filePath = Application.persistentDataPath + m_FilePath;

        if (!File.Exists(filePath))
        {
            BestTimeText.text = "최고 기록: 00:00:00";
        }
        else
        {
            string[] times = File.ReadAllLines(filePath);
            if (times.Length <= 0)
            {
                BestTimeText.text = "최고 기록: 00:00:00";
            }
            else
            {
                foreach (string time in times)
                {
                    TimeSpan recordedTime;
                    if (TimeSpan.TryParse(time, out recordedTime))
                    {
                        if (recordedTime < bestTime)
                        {
                            bestTime = recordedTime;
                        }
                    }
                }

                BestTimeText.text = "최고 기록: " + FormatTime(bestTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameState == GameState.Run)
        {
            TimeSpan timeSpan = watch.Elapsed;
            string formattedTime = FormatTime(timeSpan);

            // UI Text 업데이트
            StopWatchText.text = formattedTime;
        }
        
    }

    public void OpenOption()
    {
        m_GameState = GameState.Pause;
        watch.Stop();
        gameOption.SetActive(true);
        Time.timeScale = 0f;
        m_GameState = GameState.Pause;
    }

    public void CloseOption()
    {
        m_GameState = GameState.Run;
        watch.Start();
        gameOption.SetActive(false);
        Time.timeScale = 1f;
        m_GameState = GameState.Run;
    }

    public void RestartGame()
    {
        m_GameState = GameState.Run;
        watch.Reset();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameClear()
    {
        m_GameState = GameState.GameClear;
        watch.Stop();
        if (!m_HasSaved)
        {
            TimeSpan timeSpan = watch.Elapsed;
            string formattedTime = FormatTime(timeSpan);
            string filePath = Application.persistentDataPath + m_FilePath;
            File.AppendAllText(filePath, formattedTime + "\n");
            m_HasSaved = true;
        }

        QuitGame();
    }

    string FormatTime(TimeSpan timeSpan)
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}",
                             timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
}
