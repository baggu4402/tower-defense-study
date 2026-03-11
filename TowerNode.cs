using UnityEngine;

/*
역할
- 타워를 설치할 수 있는 위치를 담당
- 클릭되면 BuildMenuUI를 열어줌
- 선택된 타워 프리팹을 실제로 설치함

게임 흐름
TowerNode 클릭
    ↓
BuildMenuUI 열기
    ↓
타워 선택
    ↓
BuildTower(prefab)
    ↓
현재 위치에 타워 생성

이 코드에서 공부할 핵심 개념
1. OnMouseDown()
2. FindFirstObjectByType<T>()
3. 오브젝트 생성 (Instantiate)
4. 상태 확인 (이미 설치되었는지)
5. 책임 분리
   - UI는 선택 담당
   - Node는 실제 설치 담당
*/
public class TowerNode : MonoBehaviour
{
    [Header("Installed Tower")]
    // 현재 이 노드에 설치된 타워
    // 없으면 null
    public GameObject currentTower;
    // BuildMenuUI를 참조하여 메뉴를 열기 위해 사용
    private BuildMenuUI buildMenuUI;
    /*
    Start()

    씬 안에 있는 BuildMenuUI를 찾아서 저장

    FindFirstObjectByType<T>()
    씬에서 해당 타입의 오브젝트를 하나 찾아오는 함수
    */
    private void Start()
    {
        buildMenuUI = FindFirstObjectByType<BuildMenuUI>();
    }
    /*
    OnMouseDown()

    마우스로 이 오브젝트를 클릭하면 호출되는 Unity 함수

    역할
    - BuildMenuUI가 있는지 확인
    - 메뉴 열기
    */
    private void OnMouseDown()
    {
        if (buildMenuUI == null)
        {
            Debug.LogError("BuildMenuUI를 찾을 수 없습니다.");
            return;
        }

        buildMenuUI.OpenMenu(this);
    }
    /*
    BuildTower(GameObject towerPrefab)

    역할
    - 이미 타워가 있는지 확인
    - 없으면 현재 노드 위치에 타워 생성
    - 생성된 타워를 currentTower에 저장

    이렇게 저장하는 이유  ==> 추가예정?임
    - 같은 자리에 중복 설치 방지
    - 나중에 업그레이드/판매 기능 구현 가능
    */
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
    /*
    현재 노드에 타워가 있는지 확인

    true  -> 설치됨
    false -> 비어 있음
    */
    public bool HasTower()
    {
        return currentTower != null;
    }

}
