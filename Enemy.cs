using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Stats")]
    public int maxHP = 5;
    private int currentHP;

    private Path path;
    private int currentWaypointIndex = 0;
    private bool isDead = false;

    public void Setup(Path assignedPath)
    {
        path = assignedPath;

        if (path == null || path.WaypointCount() == 0)
        {
            Debug.LogError("Path가 없거나 waypoint가 없습니다.");
            return;
        }

        currentHP = maxHP;

        // 0을 웨이 첫번째 포인트로 설정
        transform.position = path.GetWaypoint(0).position;
        currentWaypointIndex = 1;
    }

    private void Update()
    {
        if (path == null || isDead)
            return;

        MoveAlongPath();

        // 테스트용: 키를 누르면 데미지 받기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    private void MoveAlongPath()
    {
        if (currentWaypointIndex >= path.WaypointCount())
        {
            ReachGoal();
            return;
        }

        Transform targetWaypoint = path.GetWaypoint(currentWaypointIndex);

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetWaypoint.position,
            moveSpeed * Time.deltaTime
        );

        float distance = Vector2.Distance(transform.position, targetWaypoint.position);

        if (distance < 0.05f)
        {
            currentWaypointIndex++;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHP -= damage;
        Debug.Log($"{gameObject.name} 이(가) {damage} 데미지를 받음. 현재 HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} 사망!");
        Destroy(gameObject);
    }

    private void ReachGoal()
    {
        Debug.Log("적이 도착지점에 도착했습니다.");
        Destroy(gameObject);
    }
}