using System.Collections;
using UnityEngine;

public class MageUnit : MonoBehaviour
{
    [Header("Targeting")]
    public float attackRange = 3.8f;
    public LayerMask enemyLayer;

    [Header("Attack")]
    public int damage = 3;
    public float attackRate = 0.7f;
    public float attackHitDelay = 0.35f;
    public float attackLockTime = 0.7f;

    [Header("Splash")]
    public float splashRadius = 0.9f;

    [Header("Effect")]
    public GameObject impactEffectPrefab;
    public Vector3 impactOffset = new Vector3(0f, 0.5f, 0f);

    [Header("Animation")]
    public Animator animator;
    public Transform visualRoot;

    private Enemy currentTarget;
    private float attackTimer = 0f;
    private bool isAttacking = false;

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (currentTarget == null || currentTarget.IsDead() || !IsTargetInRange(currentTarget))
        {
            currentTarget = FindNearestTarget();
        }

        if (currentTarget == null)
            return;

        // 여기서도 다시 사거리 확인
        if (!IsTargetInRange(currentTarget))
            return;

        FaceTarget(currentTarget.transform.position);

        if (isAttacking)
            return;

        if (attackTimer >= 1f / attackRate)
        {
            StartCoroutine(AttackRoutine());
            attackTimer = 0f;
        }
    }

    private Enemy FindNearestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        Enemy nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();

            if (enemy == null)
            {
                continue;
            }

            if (enemy.IsDead())
                continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }

    private bool IsTargetInRange(Enemy target)
    {
        if (target == null) return false;
        return Vector2.Distance(transform.position, target.transform.position) <= attackRange;
    }

    private IEnumerator AttackRoutine()
    {
        if (currentTarget == null || currentTarget.IsDead() || !IsTargetInRange(currentTarget))
            yield break;

        isAttacking = true;

        FaceTarget(currentTarget.transform.position);
        PlayAttackAnimation();

        yield return new WaitForSeconds(attackHitDelay);

        if (currentTarget != null && !currentTarget.IsDead() && IsTargetInRange(currentTarget))
        {
            Vector3 hitPoint = currentTarget.transform.position + impactOffset;
            SpawnImpactEffect(hitPoint);
            DealSplashDamage(hitPoint);
        }

        yield return new WaitForSeconds(Mathf.Max(0f, attackLockTime - attackHitDelay));

        isAttacking = false;
    }

    private void DealSplashDamage(Vector3 hitPoint)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitPoint, splashRadius, enemyLayer);
        Debug.Log($"[MageUnit] 범위 데미지 대상 수: {hits.Length}");

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy == null || enemy.IsDead())
                continue;

            Debug.Log($"[MageUnit] {enemy.name} 에게 {damage} 데미지");
            enemy.TakeDamage(damage);
        }
    }

    private void SpawnImpactEffect(Vector3 hitPoint)
    {
        if (impactEffectPrefab == null)
        {
            Debug.LogWarning("[MageUnit] impactEffectPrefab 비어 있음");
            return;
        }

        Instantiate(impactEffectPrefab, hitPoint, Quaternion.identity);
    }

    private void PlayAttackAnimation()
    {
        if (animator == null)
        {
            Debug.LogWarning("[MageUnit] animator 연결 안 됨");
            return;
        }

        Debug.Log("[MageUnit] Attack 트리거 실행");
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
    }

    private void FaceTarget(Vector2 targetPos)
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
}