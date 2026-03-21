using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public int playerLife = 20;
    public int gold = 200;

    [Header("UI")]
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI goldText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateUI();
    }

    public bool HasEnoughGold(int amount)
    {
        return gold >= amount;
    }

    public bool SpendGold(int amount)
    {
        if (isGameOver)
            return false;

        if (gold < amount)
        {
            Debug.Log($"골드 부족! 현재 골드: {gold}, 필요 골드: {amount}");
            return false;
        }

        gold -= amount;
        UpdateUI();

        Debug.Log($"골드 사용: {amount}, 남은 골드: {gold}");
        return true;
    }

    public void AddGold(int amount)
    {
        if (isGameOver)
            return;

        gold += amount;
        UpdateUI();

        Debug.Log($"골드 획득: {amount}, 현재 골드: {gold}");
    }

    public void LoseLife(int amount)
    {
        if (isGameOver)
            return;

        playerLife -= amount;

        if (playerLife < 0)
            playerLife = 0;

        UpdateUI();

        Debug.Log($"생명 감소: {amount}, 남은 생명: {playerLife}");

        if (playerLife <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("게임 오버!");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = "GAME OVER";
    }

    private void UpdateUI()
    {
        if (lifeText != null)
            lifeText.text = $"{playerLife}";

        if (goldText != null)
            goldText.text = $"{gold}";
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}