using UnityEngine;
/*
역할
- 플레이어가 설치할 타워 종류를 선택하는 UI 관리
- 특정 TowerNode를 선택했을 때 메뉴를 열어줌
- 선택한 타워 프리팹을 해당 노드에 설치하도록 연결

게임 흐름
TowerNode 클릭
    ↓
BuildMenuUI.OpenMenu(node)
    ↓
메뉴 표시
    ↓
버튼 클릭 (Archer / Mage / Artillery / Barracks)
    ↓
selectedNode.BuildTower(prefab)
    ↓
타워 설치

이 코드에서 공부할 핵심 개념
1. UI 패널 열기/닫기
2. GameObject 참조
3. 특정 오브젝트 기억하기 (selectedNode)
4. 버튼 함수와 게임 로직 연결
5. Instantiate를 간접적으로 호출하는 구조
*/
public class BuildMenuUI : MonoBehaviour
{
    [Header("Tower Prefabs")]
    public GameObject archerTowerPrefab;
    public GameObject mageTowerPrefab;
    public GameObject artilleryTowerPrefab;
    public GameObject barracksTowerPrefab;

    [Header("UI")]
    public GameObject menuPanel;
    
    // 현재 플레이어가 클릭해서 선택한 설치 지점
    // 어떤 노드에 타워를 지을지 기억하는 변수
    private TowerNode selectedNode;

    // 패널꺼놧다가 나중에 보이게 함
    private void Start()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }

    /*
    OpenMenu(TowerNode node)

    TowerNode를 클릭했을 때 호출됨

    역할
    1. 어떤 노드를 클릭했는지 저장
    2. 메뉴 패널 표시
    3. 패널 위치를 중앙으로 설정
    */
    public void OpenMenu(TowerNode node)
    {
        selectedNode = node;

        if (menuPanel != null)
        {
            menuPanel.SetActive(true);

            // 화면 중앙에 고정으로 띄우기
            // 나중에는 노드위에 표시할거임 지금은 테스트용
            RectTransform rect = menuPanel.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
        }
    }

    // 패널 숨기긔
    public void CloseMenu()
    {
        selectedNode = null;

        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }
    
    /*
    아래 함수들은 UI 버튼 OnClick()에 연결되는 함수들

    흐름
    버튼 클릭
      ↓
    selectedNode가 null이 아닌지 확인
      ↓
    해당 프리팹 설치 요청
      ↓
    메뉴 닫기
    */
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

