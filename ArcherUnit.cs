using System.Collections;
using UnityEngine;

public class ArcherUnit : MonoBehaviour
{
    [Header("Attack Stats")]
    public float attackRange = 2.5f;
    public float attackRate = 1f;
    public int damage = 1;

    [Header("Arrow")]
    public ArrowProjectile arrowPrefab;
    public Transform firePoint;
    public float arrowFireDelay = 0.25f;   // 화살이 실제로 나가는 지연 시간

    [Header("Visual")]
    public Transform visualRoot;
    public Animator animator;

    private float attackTimer = 0f;
    private bool isAttacking = false;

    private void Update()
    {
        attackTimer += Time.deltaTime;

        Enemy target = FindTarget();

        if (target != null)
        {
            FaceTarget(target);
        }

        if (target != null && !isAttacking && attackTimer >= 1f / attackRate)
        {
            StartCoroutine(AttackRoutine(target));
            attackTimer = 0f;
        }
    }

    private Enemy FindTarget()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= attackRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void FaceTarget(Enemy target)
    {
        if (target == null || visualRoot == null)
            return;

        Vector3 scale = visualRoot.localScale;

        if (target.transform.position.x < transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);

        visualRoot.localScale = scale;
    }

    private IEnumerator AttackRoutine(Enemy target)
    {
        isAttacking = true;

        if (target == null)
        {
            isAttacking = false;
            yield break;
        }

        FaceTarget(target);

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        yield return new WaitForSeconds(arrowFireDelay);

        // 기다리는 동안 적이 죽었을 수 있으니 다시 체크
        if (target != null)
        {
            FireArrow(target);
        }

        isAttacking = false;
    }

    private void FireArrow(Enemy target)
    {
        if (target == null) return;

        if (arrowPrefab == null)
        {
            Debug.LogError("Arrow Prefab이 연결되지 않았습니다.");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogError("Fire Point가 연결되지 않았습니다.");
            return;
        }

        ArrowProjectile arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.Setup(target, damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}