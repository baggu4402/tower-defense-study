using UnityEngine;

public class TowerNode : MonoBehaviour
{
    [Header("Installed Tower")]
    public GameObject currentTower;

    private BuildMenuUI buildMenuUI;

    private void Start()
    {
        buildMenuUI = FindFirstObjectByType<BuildMenuUI>();
    }

    private void OnMouseDown()
    {
        if (buildMenuUI == null)
        {
            Debug.LogError("BuildMenuUI를 찾을 수 없습니다.");
            return;
        }

        buildMenuUI.OpenMenu(this);
    }

    public void BuildTower(GameObject towerPrefab)
    {
        if (currentTower != null)
        {
            Debug.Log("이미 타워가 설치되어 있습니다.");
            return;
        }

        currentTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        Debug.Log($"{towerPrefab.name} 설치 완료!");
    }

    public bool HasTower()
    {
        return currentTower != null;
    }
}