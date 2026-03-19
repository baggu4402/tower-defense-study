using UnityEngine;

public class SoldierUnit : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 2f;
    public float stopDistance = 0.05f;

    [Header("HP")]
    public int maxHP = 5;
    private int currentHP;
    private bool isDead = false;

    [Header("Combat")]
    public float detectRange = 1.2f;
    public float attackRange = 0.25f;
    public float attackRate = 1f;
    public int damage = 1;

    [Header("Animation")]
    public Animator animator;
    public Transform visualRoot;

    private Vector2 targetPosition;
    private bool isInitialized = false;

    private Enemy currentEnemy;
    private float attackTimer = 0f;

    private BarracksBuilding ownerBarracks;
    private int formationIndex = -1;

    public void Setup(Vector2 startTargetPosition)
    {
        targetPosition = startTargetPosition;
        currentHP = maxHP;
        isDead = false;
        isInitialized = true;
    }

    public void SetOwner(BarracksBuilding barracks, int index)
    {
        ownerBarracks = barracks;
        formationIndex = index;
    }

    public int GetFormationIndex()
    {
        return formationIndex;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void SetTargetPosition(Vector2 newTargetPosition)
    {
        targetPosition = newTargetPosition;

        if (currentEnemy != null)
        {
            currentEnemy.ReleaseBlock(this);
            currentEnemy = null;
        }
    }

    private void Update()
    {
        if (!isInitialized || isDead)
            return;

        attackTimer += Time.deltaTime;

        if (currentEnemy == null)
        {
            FindEnemy();

            if (currentEnemy == null)
            {
                MoveToTargetPosition();
                return;
            }
        }

        if (currentEnemy == null || currentEnemy.IsDead())
        {
            currentEnemy = null;
            SetMovingAnimation(false);
            MoveToTargetPosition();
            return;
        }

        EngageEnemy();
    }

    private void MoveToTargetPosition()
    {
        float distance = Vector2.Distance(transform.position, targetPosition);

        if (distance <= stopDistance)
        {
            SetMovingAnimation(false);
            return;
        }

        FaceDirection(targetPosition);
        SetMovingAnimation(true);

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    private void FindEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            if (enemy == null || enemy.IsDead())
                continue;

            if (enemy.IsBlocked())
                continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= detectRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            currentEnemy = closestEnemy;
            currentEnemy.SetBlocked(this);
        }
    }

    private void EngageEnemy()
    {
        if (currentEnemy == null)
            return;

        float distance = Vector2.Distance(transform.position, currentEnemy.transform.position);

        FaceDirection(currentEnemy.transform.position);

        if (distance > attackRange)
        {
            SetMovingAnimation(true);

            transform.position = Vector2.MoveTowards(
                transform.position,
                currentEnemy.transform.position,
                moveSpeed * Time.deltaTime
            );
            return;
        }

        SetMovingAnimation(false);

        if (attackTimer >= 1f / attackRate)
        {
            PlayAttackAnimation();
            currentEnemy.TakeDamage(damage);
            attackTimer = 0f;
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isDead)
            return;

        currentHP -= dmg;
        Debug.Log($"{gameObject.name} 이(가) {dmg} 데미지를 받음. 현재 HP: {currentHP}");

        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (currentEnemy != null)
        {
            currentEnemy.ReleaseBlock(this);
            currentEnemy = null;
        }

        SetMovingAnimation(false);

        if (ownerBarracks != null)
            ownerBarracks.OnSoldierDead(this, formationIndex);

        Destroy(gameObject);
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

    private void SetMovingAnimation(bool isMoving)
    {
        if (animator != null)
            animator.SetBool("IsMoving", isMoving);
    }

    private void PlayAttackAnimation()
    {
        if (animator != null)
            animator.SetTrigger("Attack");
    }

    public void ReleaseEnemy()
    {
        currentEnemy = null;
        SetMovingAnimation(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}