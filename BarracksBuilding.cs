using System.Collections.Generic;
using UnityEngine;

public class BarracksBuilding : MonoBehaviour
{
    [Header("Soldier Spawn")]
    public SoldierUnit soldierPrefab;

    [Header("Rally Point")]
    public Vector2 rallyPoint;

    [Header("Soldier Formation Offsets")]
    public Vector2[] soldierOffsets;

    private List<SoldierUnit> spawnedSoldiers = new List<SoldierUnit>();

    private void Start()
    {
        rallyPoint = (Vector2)transform.position + new Vector2(0f, -1.2f);
        SpawnSoldiers();
    }

    private void OnMouseDown()
    {
        if (BarracksRallyController.Instance != null)
        {
            BarracksRallyController.Instance.SelectBarracks(this);
        }
    }

    public void SpawnSoldiers()
    {
        if (soldierPrefab == null)
        {
            Debug.LogError("Soldier Prefab이 연결되지 않았습니다.");
            return;
        }

        if (soldierOffsets == null || soldierOffsets.Length == 0)
        {
            Debug.LogError("Soldier Offsets가 설정되지 않았습니다.");
            return;
        }

        for (int i = 0; i < soldierOffsets.Length; i++)
        {
            Vector2 soldierTargetPos = rallyPoint + soldierOffsets[i];

            SoldierUnit soldier = Instantiate(
                soldierPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );

            soldier.Setup(soldierTargetPos);
            spawnedSoldiers.Add(soldier);
        }
    }

    public void SetRallyPoint(Vector2 newRallyPoint)
    {
        rallyPoint = newRallyPoint;
        UpdateSoldierPositions();
    }

    private void UpdateSoldierPositions()
    {
        for (int i = 0; i < spawnedSoldiers.Count; i++)
        {
            if (spawnedSoldiers[i] == null)
                continue;

            if (i >= soldierOffsets.Length)
                continue;

            Vector2 newTargetPos = rallyPoint + soldierOffsets[i];
            spawnedSoldiers[i].SetTargetPosition(newTargetPos);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(rallyPoint, 0.12f);

        if (soldierOffsets != null)
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < soldierOffsets.Length; i++)
            {
                Vector2 pos = rallyPoint + soldierOffsets[i];
                Gizmos.DrawWireSphere(pos, 0.1f);
            }
        }
    }
}
