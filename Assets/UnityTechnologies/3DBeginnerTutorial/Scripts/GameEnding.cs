using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      // 씬을 처리하는 네임스페이스 추가
using System.Diagnostics;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;     // 페이드가 끝남과 동시에 게임이 종료되지 않고 약간의 틈을 주기 위해

    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;        // 플레이어가 잡혔을 때 나타날 이미지
    public AudioSource exitAudio;
    public AudioSource caughtAudio;


    bool m_isPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 게임을 클리어했을 경우 
        if (m_isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        // 플레이어가 적에게 잡혔을 경우
        else if(m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 박스 콜라이더와 충돌했을 경우에 엔딩이 트리거 
        if(other.gameObject == player)
        {
            m_isPlayerAtExit = true;
        }    
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    // 엔딩 화면 보여준 후 게임을 종료 
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                //SceneManager.LoadScene(0);      // 메인 씬(현재 씬)을 다시 로드
                GameManager.gameManager.RestartGame();
            }
            else
            {
                //Application.Quit();     // 빌드시에만 작동 
                GameManager.gameManager.GameClear();
            }
        }
    }
}
