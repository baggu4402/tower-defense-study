using UnityEngine;

/*
역할
- 적 캐릭터의 이동, 체력, 사망 처리를 담당
- Path 시스템의 waypoint를 따라 이동

이 코드에서 공부할 핵심 개념
1. MonoBehaviour
2. Update() 게임 루프
3. Transform과 위치 이동
4. Vector2.MoveTowards
5. Time.deltaTime
6. 상태 관리 (isDead)
7. 캡슐화(private 변수)

흐름
EnemySpawner
    ↓
Enemy 생성
    ↓
Setup(Path)
    ↓
Path waypoint 따라 이동
    ↓
체력 감소 → 죽음
*/

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    // 적 이동 속도
    // MoveTowards 계산에 사용
    public float moveSpeed = 2f;

    [Header("Stats")]
    public int maxHP = 5;
    private int currentHP;

    // Enemy가 따라갈 경로
    private Path path;

    //waypoint index
    private int currentWaypointIndex = 0;


    // 적이 죽엇는지 확인
    // 죽은상태에서 업데이트 로직이 실행되지 않게 만듦
    private bool isDead = false;


    /*
    Setup 함수

    EnemySpawner가 Enemy를 생성한 뒤
    Path 정보를 전달하기 위해 사용

    의존성 주입(Dependency Injection) 개념
    외부 시스템이 필요한 데이터를 전달
    */
    public void Setup(Path assignedPath)
    {
        // 전달받은 path저장
        path = assignedPath;
        // path가 없거나 waypoint없으면 오류
        if (path == null || path.WaypointCount() == 0)
        {
            Debug.LogError("Path가 없거나 waypoint가 없습니다.");
            return;
        }
        // 채력초기화
        currentHP = maxHP;

        // 적 시작위치 설정
        // 첫번째 waypoint 위치로 이동
        transform.position = path.GetWaypoint(0).position;
        // 다음 waypoint를 목표로 성정
        currentWaypointIndex = 1;
    }

    /*
    Update()

    Unity의 메인 게임 루프
    매 프레임마다 실행됨

    사용 목적
    - Enemy 이동
    - 상태 체크
    */
    private void Update()
    {
        // path가 없거나 죽은 상태면 아무것도 안하긔
        if (path == null || isDead)
            return;
        // 경로따라 이동 함수
        MoveAlongPath();

        // 테스트용: 키를 누르면 데미지 받기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    /*
    Enemy 이동 시스템

    로직
    1. 다음 waypoint 확인
    2. MoveTowards로 이동
    3. waypoint 도착 체크
    4. 다음 waypoint로 변경
    */
    private void MoveAlongPath()
    {
        // 마지막 waypoint에 도달하면
        if (currentWaypointIndex >= path.WaypointCount())
        {
            ReachGoal();
            return;
        }
        // 목표 waypoint 가져오기
        Transform targetWaypoint = path.GetWaypoint(currentWaypointIndex);

        /*
        MoveTowards

        현재 위치 → 목표 위치로 이동

        Time.deltaTime
        프레임 속도와 상관없이
        동일한 이동 속도를 유지하도록 하는 값
        */
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetWaypoint.position,
            moveSpeed * Time.deltaTime
        );
        
        // 목표 waypoint까지 거리 계산
        float distance = Vector2.Distance(transform.position, targetWaypoint.position);
        // waypoint에 거의 도착했으면
        if (distance < 0.05f)
        {
            // 다음 웨이포인트로 이동 인덱스 추가해서 ㄱㄱ
            currentWaypointIndex++;
        }
    }

    /*
    Enemy 데미지 처리

    Tower나 Bullet이 호출하게 될 함수
    */
    public void TakeDamage(int damage)
    {
        // 이미 죽은 경우 처리 안함
        if (isDead)
            return;
        // 체력 감소
        currentHP -= damage;
        Debug.Log($"{gameObject.name} 이(가) {damage} 데미지를 받음. 현재 HP: {currentHP}");
        // 체력이 0이면 죽어야지
        if (currentHP <= 0)
        {
            Die();
        }
    }

    /*
    Enemy 사망 처리

    실제 게임에서는
    - 골드 지급
    - 사망 이펙트
    - 사운드
    등이 추가 예정
    */
    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} 사망!");
        Destroy(gameObject);
    }
    /*
    Enemy가 목적지에 도착했을 때

    실제 디펜스 게임에서는
    - 플레이어 체력 감소
    - UI 업데이트
    등이 추가 예정
    */
    private void ReachGoal()
    {
        Debug.Log("적이 도착지점에 도착했습니다.");
        Destroy(gameObject);
    }

}
