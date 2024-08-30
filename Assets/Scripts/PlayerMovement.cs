using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float turnSpeed = 20f;     // 캐릭터 회전 속도

    Vector3 m_Movement;         // 이동 벡터 변수: player의 움직임을 결정
    Quaternion m_Rotation = Quaternion.identity;    // 회전을 저장 (이동이 없을 때 회전 값을 주지 않음)
    Animator m_Animator;        // 애니메이터
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    GameObject m_PlayerName;



    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        m_PlayerName = GameObject.Find("NicknameCanvas/JohnLemon");

    }

    // Update is called once per frame
    // Update : 렌더링된 프레임에 맞춰 호출
    // FixedUpdate: 물리 시스템의 상호작용을 계산하기 전에 호출(초당 50회)
    void FixedUpdate()
    {
        // 수평, 수직 축 값 저장 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 벡터 변수에 변경될 수직, 수평 값 저장 
        m_Movement.Set(horizontal, 0f, vertical);
        // 이동 벡터가 항상 같은 크기를 유지하도록 정규화(벡터의 방향은 동일하게 유지하면서 크기를 1로 변경)
        m_Movement.Normalize();

        // 수평, 수직 입력이 있는지 여부 반환
        // Approximately: 매개변수를 비교했을 때 같으면 true, 다르면 false를 반환 
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        if(isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        // 캐릭터 전방 벡터 계산
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);   // 파라미터가 바라보는 방향으로 회전 생성

        m_PlayerName.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));

    }

    // 애니메이터에서 루트 모션의 적용 방식 변경: 이동과 회전을 개별적으로 적용하기 위해 
    void OnAnimatorMove()
    {
        // m_Rigidbody.position : 새로운 위치
        // m_Animator.deltaPosition.magnitude: 루트 모션으로 인한 프레임당 위치 이동방향을 나타내는 이동 벡터
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);       // 회전 설정
    }
}
