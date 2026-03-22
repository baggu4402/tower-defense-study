using UnityEngine;
using UnityEngine.UI;

public class UIButtonPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseSpeed = 3f;
    public float pulseAmount = 0.08f;

    private Vector3 originalScale;
    private Button button;

    private void Awake()
    {
        originalScale = transform.localScale;
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        transform.localScale = originalScale;
    }

    private void Update()
    {
        if (button != null && !button.interactable)
        {
            transform.localScale = originalScale;
            return;
        }

        float scaleOffset = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * scaleOffset;
    }
}