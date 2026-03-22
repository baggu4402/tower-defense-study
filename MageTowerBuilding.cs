using UnityEngine;

public class MageTowerBuilding : MonoBehaviour
{
    [Header("Mage Spawn")]
    public MageUnit mageUnitPrefab;
    public Transform mageSpawnPoint;

    private MageUnit currentMage;

    private void Start()
    {
        SpawnMage();
    }

    public void SpawnMage()
    {
        if (mageUnitPrefab == null)
        {
            Debug.LogError("MageUnit 프리팹이 연결되지 않았습니다.");
            return;
        }

        if (mageSpawnPoint == null)
        {
            Debug.LogError("Mage Spawn Point가 없습니다.");
            return;
        }

        if (currentMage != null)
            return;

        currentMage = Instantiate(mageUnitPrefab, mageSpawnPoint.position, Quaternion.identity, transform);
    }
}