using UnityEngine;

public class BarracksRallyController : MonoBehaviour
{
    /*
    Singleton 패턴

    어디서든

    BarracksRallyController.Instance

    로 접근 가능
    */
    public static BarracksRallyController Instance;

    // 현재 선택된 병영
    private BarracksBuilding selectedBarracks;

    private void Awake()
    {
        Instance = this;
    }
    
    /*
    Update()

    병영이 선택된 상태에서

    우클릭하면

    새로운 rallyPoint 지정
    */
    private void Update()
    {
        if (selectedBarracks == null)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 집결지 설정
            selectedBarracks.SetRallyPoint(worldPos);
            
            selectedBarracks = null;
        }
    }

    public void SelectBarracks(BarracksBuilding barracks)
    {
        selectedBarracks = barracks;
        Debug.Log("집결지 지정 모드: 우클릭으로 위치 선택");
    }
}
