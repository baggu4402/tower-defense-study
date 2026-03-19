using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Stats")]
    public int maxHP = 5;
    private int currentHP;

    [Header("Reward / Penalty")]
    public int rewardGold = 10;
    public int damageToLife = 1;

    [Header("Combat")]
    public int damage = 1;
    public float attackRate = 1f;
    public float attackHitDelay = 0.2f;
    public float attackLockTime = 0.5f;
    private float attackTimer = 0f;
    private bool isAttacking = false;

    [Header("Animation")]
    public Animator animator;
    public Transform visualRoot;

    [Header("Hit Effect")]
    public float hitFlashDuration = 0.1f;

    private Path path;
    private int currentWaypointIndex = 0;
    private bool isDead = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

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

        attackTimer += Time.deltaTime;

        if (isBlocked)
        {
            SetMovingAnimation(false);
            FaceCurrentTarget();
            AttackSoldier();
        }
        else
        {
            isAttacking = false;
            SetMovingAnimation(true);
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

        FaceDirection(targetWaypoint.position);

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

    private void AttackSoldier()
    {
        if (currentBlocker == null || currentBlocker.IsDead())
        {
            isBlocked = false;
            currentBlocker = null;
            isAttacking = false;
            attackTimer = 0f;
            return;
        }

        if (isAttacking)
            return;

        if (attackTimer >= 1f / attackRate)
        {
            StartCoroutine(AttackRoutine());
            attackTimer = 0f;
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        PlayAttackAnimation();

        yield return new WaitForSeconds(attackHitDelay);

        if (!isDead && currentBlocker != null && !currentBlocker.IsDead())
        {
            currentBlocker.TakeDamage(damage);
        }

        yield return new WaitForSeconds(Mathf.Max(0f, attackLockTime - attackHitDelay));

        isAttacking = false;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        currentHP -= damageAmount;
        Debug.Log($"{gameObject.name} 이(가) {damageAmount} 데미지를 받음. 현재 HP: {currentHP}");

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
        if (isDead)
            return;

        isDead = true;
        isAttacking = false;
        SetMovingAnimation(false);

        if (currentBlocker != null)
        {
            currentBlocker.ReleaseEnemy();
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddGold(rewardGold);
        }

        Destroy(gameObject);
    }

    private void ReachGoal()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife(damageToLife);
        }

        Destroy(gameObject);
    }

    public void SetBlocked(SoldierUnit soldier)
    {
        if (isDead)
            return;

        isBlocked = true;
        currentBlocker = soldier;
        attackTimer = 0f;
    }

    public void ReleaseBlock(SoldierUnit soldier)
    {
        if (currentBlocker == soldier)
        {
            isBlocked = false;
            currentBlocker = null;
            isAttacking = false;
            attackTimer = 0f;
            StopAllCoroutines();
        }
    }

    private void SetMovingAnimation(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }
    }

    private void PlayAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void FaceCurrentTarget()
    {
        if (currentBlocker == null)
            return;

        FaceDirection(currentBlocker.transform.position);
    }

    private void FaceDirection(Vector2 targetPos)
    {
        if (visualRoot == null)
            return;

        Vector3 scale = visualRoot.localScale;

        if (targetPos.x < transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);

        visualRoot.localScale = scale;
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