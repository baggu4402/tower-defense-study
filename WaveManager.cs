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
            Enemy spawnedEnemy = enemySpawner.SpawnEnemy();

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