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
    // 캐릭터 그래픽
    public Transform visualRoot;
    public Animator animator;
    // 공격 쿨타임 계산용 타이머
    private float attackTimer = 0f;
    // 공격 중인지 여부
    private bool isAttacking = false;

    /*
    Update()

    매 프레임 실행

    역할
    1. 공격 타이머 증가
    2. 사거리 내 적 탐색
    3. 적 방향 바라보기
    4. 공격 조건 만족 시 공격 시작
    */
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
    /*
    FindTarget()

    씬에 있는 모든 Enemy 중
    사거리 내 가장 가까운 적을 찾음
    */
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
    
    /*
    FaceTarget()

    적 방향으로 캐릭터를 회전
    좌우 뒤집기 방식 사용
    */

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

    
    /*
    AttackRoutine()

    공격 코루틴

    흐름
    1. 공격 애니메이션 실행
    2. arrowFireDelay 만큼 대기
    3. 화살 발사
    */
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

    /*
    FireArrow()

    화살 프리팹을 생성하고
    목표 Enemy를 전달
    */
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
    // 공격 사거리 표시 (Scene 뷰 디버그)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
