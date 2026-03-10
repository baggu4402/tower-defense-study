using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Path path;

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(enemyPrefab);
        newEnemy.Setup(path);
    }
}