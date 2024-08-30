using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool m_IsPlayerInRange;
    public GameEnding gameEnding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 가시선이 명확한지 확인: 플레이어와 적 사이에 벽이 있는데도 게임 종료 등 변수를 막기 위해서
        if (m_IsPlayerInRange)
        {
            // Vetcor3: Vector(0,1,0) -> 질량의 중심을 볼 수 있도록 함
            Vector3 direction = player.position - transform.position + Vector3.up;
            
            // 레이캐스트 메서드 조건
            // 1. 레이캐스트가 어느 레이를 따라 발생하는지 정의
            // 2. 감지하고자 하는 콜라이더의 종류 제한
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            // raycastHit: 레이캐스트에 부딪히는 대상에 관한 정보로 해당 데이터를 설정, out 파라미터를 통해 정보 반환 
            if (Physics.Raycast(ray, out raycastHit))
            {
               if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
}
