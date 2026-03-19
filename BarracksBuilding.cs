using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
포함된 시스템
- Barracks (병영)
- Soldier (병사)
- Enemy (적 AI + 전투)
- Build UI
- GameManager (자원 시스템)

====================================================
*/
public class BarracksBuilding : MonoBehaviour
{
    [Header("Soldier Spawn")]
    public SoldierUnit soldierPrefab;

    [Header("Rally Point")]
    public Vector2 rallyPoint;

    [Header("Soldier Formation Offsets")]
    public Vector2[] soldierOffsets;

    [Header("Respawn")]
    public float respawnTime = 5f;

    private List<SoldierUnit> spawnedSoldiers = new List<SoldierUnit>();
    private bool[] occupiedSlots;

    private void Start()
    {
        rallyPoint = (Vector2)transform.position + new Vector2(0f, -1.2f);

        if (soldierOffsets != null && soldierOffsets.Length > 0)
            occupiedSlots = new bool[soldierOffsets.Length];

        SpawnSoldiers();
    }

    private void OnMouseDown()
    {
        if (BarracksRallyController.Instance != null)
            BarracksRallyController.Instance.SelectBarracks(this);
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
            SpawnSoldierAtIndex(i);
    }

    private void SpawnSoldierAtIndex(int index)
    {
        if (index < 0 || index >= soldierOffsets.Length)
            return;

        Vector2 soldierTargetPos = rallyPoint + soldierOffsets[index];

        SoldierUnit soldier = Instantiate(
            soldierPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );

        soldier.Setup(soldierTargetPos);
        soldier.SetOwner(this, index);

        spawnedSoldiers.Add(soldier);
        occupiedSlots[index] = true;
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

            int slotIndex = spawnedSoldiers[i].GetFormationIndex();

            if (slotIndex < 0 || slotIndex >= soldierOffsets.Length)
                continue;

            Vector2 newTargetPos = rallyPoint + soldierOffsets[slotIndex];
            spawnedSoldiers[i].SetTargetPosition(newTargetPos);
        }
    }

    public void OnSoldierDead(SoldierUnit deadSoldier, int formationIndex)
    {
        if (spawnedSoldiers.Contains(deadSoldier))
            spawnedSoldiers.Remove(deadSoldier);

        if (occupiedSlots == null)
            return;

        if (formationIndex >= 0 && formationIndex < occupiedSlots.Length)
        {
            occupiedSlots[formationIndex] = false;
            StartCoroutine(RespawnSoldier(formationIndex));
        }
    }

    private IEnumerator RespawnSoldier(int formationIndex)
    {
        yield return new WaitForSeconds(respawnTime);

        if (soldierOffsets == null)
            yield break;

        if (formationIndex < 0 || formationIndex >= soldierOffsets.Length)
            yield break;

        if (occupiedSlots[formationIndex])
            yield break;

        SpawnSoldierAtIndex(formationIndex);
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
