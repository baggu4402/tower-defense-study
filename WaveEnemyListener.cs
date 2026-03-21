using UnityEngine;

public class WaveEnemyListener : MonoBehaviour
{
    private WaveManager waveManager;
    private bool notified = false;

    public void Setup(WaveManager manager)
    {
        waveManager = manager;
    }

    private void OnDestroy()
    {
        if (notified)
            return;

        notified = true;

        if (waveManager != null)
        {
            waveManager.NotifyEnemyDeadOrExited();
        }
    }
}