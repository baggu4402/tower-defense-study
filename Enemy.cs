using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Stats")]
    public int maxHP = 5;
    private int currentHP;

    [Header("Hit Effect")]
    public float hitFlashDuration = 0.1f;

    private Path path;
    private int currentWaypointIndex = 0;
    private bool isDead = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // 병사에게 막혔는지
    private bool isBlocked = false;
    private SoldierUnit currentBlocker;

    public void Setup(Path assignedPath)
    {
        path = assignedPath;

        if (path == null || path.WaypointCount() == 0)
        {
            Debug.LogError("Path가 없거나 waypoint가 없습니다.");
            return;
        }

        currentHP = maxHP;
        transform.position = path.GetWaypoint(0).position;
        currentWaypointIndex = 1;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (path == null || isDead)
            return;

        if (!isBlocked)
        {
            MoveAlongPath();
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

        if (spriteRenderer != null)
        {
            StartCoroutine(HitFlash());
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private IEnumerator HitFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hitFlashDuration);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        isDead = true;

        // 죽을 때 막힘 해제
        if (currentBlocker != null)
        {
            currentBlocker.ReleaseEnemy();
        }

        Destroy(gameObject);
    }

    private void ReachGoal()
    {
        Destroy(gameObject);
    }

    public void SetBlocked(SoldierUnit soldier)
    {
        if (isDead)
            return;

        isBlocked = true;
        currentBlocker = soldier;
    }

    public void ReleaseBlock(SoldierUnit soldier)
    {
        if (currentBlocker == soldier)
        {
            isBlocked = false;
            currentBlocker = null;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool IsBlocked()
    {
        return isBlocked;
    }
}