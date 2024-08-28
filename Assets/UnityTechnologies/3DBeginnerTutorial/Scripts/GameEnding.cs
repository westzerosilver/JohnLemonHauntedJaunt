using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;     // 페이드가 끝남과 동시에 게임이 종료되지 않고 약간의 틈을 주기 위해 
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    bool m_isPlayerAtExit;
    float m_Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isPlayerAtExit)
        {
            EndLevel();
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

    // 엔딩 화면 보여준 후 게임을 종료 
    void EndLevel()
    {
        m_Timer += Time.deltaTime;
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            Application.Quit();     // 빌드시에만 작동 
        }
    }
}
