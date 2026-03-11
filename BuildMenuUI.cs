using UnityEngine;

public class BuildMenuUI : MonoBehaviour
{
    [Header("Tower Prefabs")]
    public GameObject archerTowerPrefab;
    public GameObject mageTowerPrefab;
    public GameObject artilleryTowerPrefab;
    public GameObject barracksTowerPrefab;

    [Header("UI")]
    public GameObject menuPanel;

    private TowerNode selectedNode;

    private void Start()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }

    public void OpenMenu(TowerNode node)
    {
        selectedNode = node;

        if (menuPanel != null)
        {
            menuPanel.SetActive(true);

            // 화면 중앙에 고정으로 띄우기
            RectTransform rect = menuPanel.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
        }
    }

    public void CloseMenu()
    {
        selectedNode = null;

        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }

    public void BuildArcher()
    {
        if (selectedNode != null)
        {
            selectedNode.BuildTower(archerTowerPrefab);
        }
        CloseMenu();
    }

    public void BuildMage()
    {
        if (selectedNode != null)
        {
            selectedNode.BuildTower(mageTowerPrefab);
        }
        CloseMenu();
    }

    public void BuildArtillery()
    {
        if (selectedNode != null)
        {
            selectedNode.BuildTower(artilleryTowerPrefab);
        }
        CloseMenu();
    }

    public void BuildBarracks()
    {
        if (selectedNode != null)
        {
            selectedNode.BuildTower(barracksTowerPrefab);
        }
        CloseMenu();
    }
}