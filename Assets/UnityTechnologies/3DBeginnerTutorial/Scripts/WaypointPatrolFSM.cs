using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrolFSM : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;       // 유령이 가지게 될 웨이포인트

    public float findDistance = 4f;

    ObserverState m_State;
    int m_CurrentWaypointIndex;

    // 상태함수 선언
    enum ObserverState
    {
        Idle,
        Move,
    }

    // Start is called before the first frame update
    void Start()
    {
        m_State = ObserverState.Idle;
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case ObserverState.Idle:
                Idle();
                break;

            case ObserverState.Move:
                Move();
                break;

        }
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = ObserverState.Move;
        }

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, player.position) > findDistance)
        {
            m_State = ObserverState.Idle;
        }
        navMeshAgent.SetDestination(player.position);
    }
}
