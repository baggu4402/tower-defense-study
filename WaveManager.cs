using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    public EnemySpawner enemySpawner;
    public TextMeshProUGUI waveText;
    public Button startWaveButton;

    [Header("Enemy Prefabs")]
    public Enemy basicEnemyPrefab;
    public Enemy fastEnemyPrefab;
    public Enemy tankEnemyPrefab;

    [Header("Wave Settings")]
    public int startWaveEnemyCount = 5;
    public int extraEnemiesPerWave = 2;
    public float spawnInterval = 0.8f;
    public float nextWaveDelay = 3f;

    private int currentWave = 0;
    private int aliveEnemies = 0;
    private bool isWaveRunning = false;
    private bool isPreparingNextWave = false;

    private void Start()
    {
        UpdateWaveUI();
        SetWaveButtonInteractable(true);
    }

    public void StartNextWave()
    {
        if (isWaveRunning || isPreparingNextWave)
            return;

        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            return;

        currentWave++;
        isWaveRunning = true;

        UpdateWaveUI();
        SetWaveButtonInteractable(false);

        StartCoroutine(SpawnWaveRoutine());
    }

    private IEnumerator SpawnWaveRoutine()
    {
        int enemyCount = startWaveEnemyCount + (currentWave - 1) * extraEnemiesPerWave;
        aliveEnemies = 0;

        for (int i = 0; i < enemyCount; i++)
        {
            Enemy enemyToSpawn = GetEnemyForCurrentWave(i);
            Enemy spawnedEnemy = enemySpawner.SpawnEnemy(enemyToSpawn);

            if (spawnedEnemy != null)
            {
                aliveEnemies++;

                WaveEnemyListener listener = spawnedEnemy.GetComponent<WaveEnemyListener>();
                if (listener == null)
                    listener = spawnedEnemy.gameObject.AddComponent<WaveEnemyListener>();

                listener.Setup(this);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Enemy GetEnemyForCurrentWave(int spawnIndex)
    {
        // Wave 1~2: 기본 적만
        if (currentWave <= 2)
        {
            return basicEnemyPrefab;
        }

        // Wave 3~4: 빠른 적 섞기
        if (currentWave <= 4)
        {
            if (spawnIndex % 3 == 0)
                return fastEnemyPrefab;

            return basicEnemyPrefab;
        }

        // Wave 5+: 빠른 적 + 탱커 적 섞기
        if (spawnIndex % 5 == 0)
            return tankEnemyPrefab;

        if (spawnIndex % 2 == 0)
            return fastEnemyPrefab;

        return basicEnemyPrefab;
    }

    public void NotifyEnemyRemoved()
    {
        aliveEnemies--;

        if (aliveEnemies <= 0 && isWaveRunning)
        {
            isWaveRunning = false;
            StartCoroutine(PrepareNextWaveRoutine());
        }
    }

    private IEnumerator PrepareNextWaveRoutine()
    {
        isPreparingNextWave = true;

        yield return new WaitForSeconds(nextWaveDelay);

        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            yield break;

        SetWaveButtonInteractable(true);
        isPreparingNextWave = false;
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave : {currentWave}";
        }
    }

    private void SetWaveButtonInteractable(bool canUse)
    {
        if (startWaveButton != null)
        {
            startWaveButton.interactable = canUse;
        }
    }
}