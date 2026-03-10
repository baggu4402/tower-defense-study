using UnityEngine;

/*
역할
- Enemy가 이동할 경로(waypoint)를 관리
- 여러 waypoint를 순서대로 저장
- Enemy가 waypoint를 하나씩 따라 이동하도록 지원

Unity 구조
Path 오브젝트
 ├ waypoint 0 (child object)
 ├ waypoint 1
 ├ waypoint 2
 └ waypoint 3

이 코드에서 공부할 핵심 개념
1. Transform
2. 배열 (Array)
3. Hierarchy 구조
4. Awake()
5. Gizmos (디버그 시각화)
6. 캡슐화 (함수를 통해 데이터 접근)

Enemy 이동 흐름
Enemy
  ↓
Path.GetWaypoint(index)
  ↓
해당 waypoint 위치로 이동
*/
public class Path : MonoBehaviour
{
    // waypoint들을 저장하는 배열
    // Enemy는 이 배열을 순서대로 이동
    public Transform[] waypoints;

    /*
    Awake()

    Unity에서 Start()보다 먼저 실행되는 함수

    사용 이유
    - Path 오브젝트의 child들을 waypoint로 자동 등록

    Hierarchy 예

    Path
      ├ Point0
      ├ Point1
      ├ Point2
      └ Point3

    transform.childCount
    현재 오브젝트의 자식 개수
    */

    private void Awake()
    {
        // 웨이포인트 배열생성 
        waypoints = new Transform[transform.childCount];
        // 모든 자식을 웨이포인트에 저장 나는 오류날까봐 미리 다 넣어둠
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    /*
    특정 waypoint 반환

    Enemy가 이동할 목표 위치를 가져올 때 사용
    */
    public Transform GetWaypoint(int index)
    {
        // 잘몬된 인덱스 방지
        if (index < 0 || index >= waypoints.Length)
            return null;

        return waypoints[index];
    }

    /*
    waypoint 개수 반환

    Enemy가 마지막 waypoint에 도달했는지
    판단할 때 사용
    */
    public int WaypointCount()
    {
        return waypoints.Length;
    }

    /*
    안해도됨!!!!!!!!!!!!!! 근데 그냥 잇으면 보기편함!!!!!!!!!!!!
    OnDrawGizmos()

    Scene 뷰에서
    waypoint 경로를 시각적으로 표시

    게임 실행과는 관계없는
    개발자 디버깅용 기능

    표시 내용
    - waypoint 위치 (Sphere)
    - waypoint 연결선 (Line)
    */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform point = transform.GetChild(i);
            // 웨이포인트 위치 표시
            Gizmos.DrawSphere(point.position, 0.15f);
            // 다음 웨이포인트로 선 연결
            if (i < transform.childCount - 1)
            {
                Transform nextPoint = transform.GetChild(i + 1);
                Gizmos.DrawLine(point.position, nextPoint.position);
            }
        }
    }

}
