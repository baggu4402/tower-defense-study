using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public int playerLife = 20;
    public int gold = 200;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool HasEnoughGold(int amount)
    {
        return gold >= amount;
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount)
        {
            Debug.Log($"골드 부족! 현재 골드: {gold}, 필요 골드: {amount}");
            return false;
        }

        gold -= amount;
        Debug.Log($"골드 사용: {amount}, 남은 골드: {gold}");
        return true;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log($"골드 획득: {amount}, 현재 골드: {gold}");
    }

    public void LoseLife(int amount)
    {
        playerLife -= amount;
        Debug.Log($"생명 감소: {amount}, 남은 생명: {playerLife}");

        if (playerLife <= 0)
        {
            playerLife = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버!");
    }
}