using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn")]
    public Enemy enemyPrefab;
    public Path path;

    private void Start()
    {
        // WaveManager가 생성 담당
    }

    public Enemy SpawnEnemy()
    {
        return SpawnEnemy(enemyPrefab);
    }

    public Enemy SpawnEnemy(Enemy prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("Spawn할 Enemy Prefab이 연결되지 않았습니다.");
            return null;
        }

        if (path == null)
        {
            Debug.LogError("Path가 연결되지 않았습니다.");
            return null;
        }

        Enemy newEnemy = Instantiate(prefab, transform.position, Quaternion.identity);
        newEnemy.Setup(path);
        return newEnemy;
    }
}