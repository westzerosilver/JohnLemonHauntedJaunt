using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       // 네비 메시 에이전트와 관련된 스트립팅 처리를 위해 추가 

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;       // 유령이 가지게 될 웨이포인트
    int m_CurrentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);    
    }

    // Update is called once per frame
    void Update()
    {
        // 네비 메시 에이전트가 목적지에 도착했는지 확인 
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
