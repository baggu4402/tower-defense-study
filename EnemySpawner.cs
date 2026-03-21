using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn")]
    public Enemy enemyPrefab;
    public Path path;

    private void Start()
    {
        // 웨이브 시스템을 쓸 거면 여기서 자동 생성하지 않음
        // SpawnEnemy();
    }

    public Enemy SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab이 연결되지 않았습니다.");
            return null;
        }

        if (path == null)
        {
            Debug.LogError("Path가 연결되지 않았습니다.");
            return null;
        }

        Enemy newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.Setup(path);
        return newEnemy;
    }
}