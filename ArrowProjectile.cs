using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [Header("Arrow Settings")]
    public float flightDuration = 0.35f;   // 도착까지 걸리는 시간
    public float arcHeight = 1.0f;         // 포물선 높이
    public int damage = 1;

    private Enemy target;

    private Vector2 startPoint;
    private Vector2 targetPoint;
    private float timer = 0f;
    private bool isFlying = false;
    /*
    Setup()

    화살이 생성될 때 호출
    목표 Enemy와 데미지 설정
    */
    public void Setup(Enemy newTarget, int newDamage)
    {
        target = newTarget;
        damage = newDamage;

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        startPoint = transform.position;
        targetPoint = target.transform.position;
        timer = 0f;
        isFlying = true;
    }
    /*
    Update()

    포물선 이동 계산
    */
    private void Update()
    {
        if (!isFlying)
            return;

        // 타겟이 먼저 죽었어도 마지막 저장 위치로는 날아가게 할지,
        // 바로 없앨지 선택 가능
        // 지금은 마지막 저장 위치까지 날아가게 둠

        timer += Time.deltaTime;
        float t = timer / flightDuration;
        t = Mathf.Clamp01(t);

        Vector2 currentTargetPoint = target != null ? (Vector2)target.transform.position : targetPoint;
        targetPoint = currentTargetPoint;

        Vector2 basePosition = Vector2.Lerp(startPoint, targetPoint, t);
        float arc = arcHeight * 4f * t * (1f - t);

        Vector2 finalPosition = basePosition + Vector2.up * arc;
        transform.position = finalPosition;

        RotateAlongPath(currentTargetPoint);

        if (t >= 1f)
        {
            HitTarget();
        }
    }

    private void RotateAlongPath(Vector2 currentTargetPoint)
    {
        Vector2 basePosition = Vector2.Lerp(startPoint, currentTargetPoint, Mathf.Clamp01(timer / flightDuration));
        float arc = arcHeight * 4f * Mathf.Clamp01(timer / flightDuration) * (1f - Mathf.Clamp01(timer / flightDuration));
        Vector2 currentPos = basePosition + Vector2.up * arc;

        float nextT = Mathf.Clamp01((timer + 0.02f) / flightDuration);
        Vector2 nextBase = Vector2.Lerp(startPoint, currentTargetPoint, nextT);
        float nextArc = arcHeight * 4f * nextT * (1f - nextT);
        Vector2 nextPos = nextBase + Vector2.up * nextArc;

        Vector2 direction = nextPos - currentPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    /*
    HitTarget()

    목표 Enemy에게 데미지 적용
    */
    private void HitTarget()
    {
        if (target != null)
        {
            target.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
