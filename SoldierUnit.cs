using UnityEngine;

public class SoldierUnit : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 2f;
    public float stopDistance = 0.05f;

    private Vector2 targetPosition;
    private bool isInitialized = false;

    public void Setup(Vector2 startTargetPosition)
    {
        targetPosition = startTargetPosition;
        isInitialized = true;
    }

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
