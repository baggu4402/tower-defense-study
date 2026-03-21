using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    public EnemySpawner enemySpawner;
    public TextMeshProUGUI waveText;

    [Header("Wave Settings")]
    public int startWaveEnemyCount = 5;
    public float spawnInterval = 0.8f;
    public float nextWaveDelay = 3f;
    public bool autoStartNextWave = true;

    private int currentWave = 0;
    private int enemiesToSpawn = 0;
    private int aliveEnemies = 0;
    private bool isWaveRunning = false;

    private void Start()
    {
        UpdateWaveUI();

        if (autoStartNextWave)
        {
            StartNextWave();
        }
    }

    public void StartNextWave()
    {
        if (isWaveRunning)
            return;

        currentWave++;
        enemiesToSpawn = startWaveEnemyCount + (currentWave - 1) * 2;
        aliveEnemies = 0;
        isWaveRunning = true;

        UpdateWaveUI();
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Enemy spawnedEnemy = enemySpawner.SpawnEnemy();

            if (spawnedEnemy != null)
            {
                aliveEnemies++;
                WaveEnemyListener listener = spawnedEnemy.gameObject.AddComponent<WaveEnemyListener>();
                listener.Setup(this);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void NotifyEnemyDeadOrExited()
    {
        aliveEnemies--;

        if (aliveEnemies <= 0 && isWaveRunning)
        {
            isWaveRunning = false;
            StartCoroutine(HandleNextWave());
        }
    }

    private IEnumerator HandleNextWave()
    {
        yield return new WaitForSeconds(nextWaveDelay);

        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            yield break;

        if (autoStartNextWave)
        {
            StartNextWave();
        }
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave : {currentWave}";
        }
    }
}