using UnityEngine;

public class BarracksRallyController : MonoBehaviour
{
    public static BarracksRallyController Instance;

    private BarracksBuilding selectedBarracks;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (selectedBarracks == null)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
