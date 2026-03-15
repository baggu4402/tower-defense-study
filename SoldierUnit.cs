using UnityEngine;

public class SoldierUnit : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 2f;
    // 목표 위치에 도착했다고 판단하는 거리
    public float stopDistance = 0.05f;

    private Vector2 targetPosition;
    // 초기화 여부
    private bool isInitialized = false;

    /*
    Setup()

    병사가 생성될 때
    목표 위치 전달
    */
    public void Setup(Vector2 startTargetPosition)
    {
        targetPosition = startTargetPosition;
        isInitialized = true;
    }
    /*
    SetTargetPosition()

    rallyPoint가 변경되면
    새로운 목표 위치로 이동
    */
    public void SetTargetPosition(Vector2 newTargetPosition)
    {
        targetPosition = newTargetPosition;
    }

    private void Update()
    {
        if (!isInitialized)
            return;

        MoveToTargetPosition();
    }
    /*
    MoveToTargetPosition()

    MoveTowards를 사용하여
    목표 위치까지 이동
    */
    private void MoveToTargetPosition()
    {
        float distance = Vector2.Distance(transform.position, targetPosition);

        if (distance <= stopDistance)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }
}
