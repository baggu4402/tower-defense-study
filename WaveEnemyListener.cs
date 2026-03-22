using UnityEngine;

public class WaveEnemyListener : MonoBehaviour
{
    private WaveManager waveManager;
    private bool hasNotified = false;

    public void Setup(WaveManager manager)
    {
        waveManager = manager;
    }

    private void OnDestroy()
    {
        if (hasNotified)
            return;

        hasNotified = true;

        if (waveManager != null)
        {
            waveManager.NotifyEnemyRemoved();
        }
    }
}