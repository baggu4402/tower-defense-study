using UnityEngine;

public class ArcherTowerBuilding : MonoBehaviour
{
    [Header("Archer Spawn")]
    public ArcherUnit archerUnitPrefab;
    public Transform archerSpawnPoint;

    private ArcherUnit currentArcher;

    private void Start()
    {
        SpawnArcher();
    }

    public void SpawnArcher()
    {
        if (archerUnitPrefab == null)
        {
            Debug.LogError("ArcherUnit 프리팹이 연결되지 않았습니다.");
            return;
        }

        if (archerSpawnPoint == null)
        {
            Debug.LogError("Archer Spawn Point가 없습니다.");
            return;
        }

        if (currentArcher != null)
            return;

        currentArcher = Instantiate(archerUnitPrefab, archerSpawnPoint.position, Quaternion.identity, transform);
    }
}