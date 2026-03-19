using UnityEngine;

public class BuildMenuUI : MonoBehaviour
{
    [Header("Tower Prefabs")]
    public GameObject archerTowerPrefab;
    public GameObject mageTowerPrefab;
    public GameObject artilleryTowerPrefab;
    public GameObject barracksTowerPrefab;

    [Header("Tower Costs")]
    public int archerCost = 70;
    public int mageCost = 100;
    public int artilleryCost = 120;
    public int barracksCost = 80;

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
            selectedNode.BuildTower(archerTowerPrefab, archerCost);
        }
        CloseMenu();
    }

    public void BuildMage()
    {
        if (selectedNode != null)
        {
            selectedNode.BuildTower(mageTowerPrefab, mageCost);
        }
        CloseMenu();
    }

    public void BuildArtillery()
    {
        if (selectedNode != null)
        {
            selectedNode.BuildTower(artilleryTowerPrefab, artilleryCost);
        }
        CloseMenu();
    }

    public void BuildBarracks()
    {
        if (selectedNode != null)
        {
            selectedNode.BuildTower(barracksTowerPrefab, barracksCost);
        }
        CloseMenu();
    }
}