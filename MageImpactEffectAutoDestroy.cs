using UnityEngine;

public class MageImpactEffectAutoDestroy : MonoBehaviour
{
    public float lifeTime = 0.5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}