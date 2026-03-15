using System.Collections.Generic;
using UnityEngine;

/*
이 시스템은 "병영 타워" 기능을 담당합니다.

구성 스크립트
1. BarracksBuilding
   - 병사를 생성
   - 집결지(rally point) 관리

2. BarracksRallyController
   - 플레이어가 집결지를 변경하도록 관리

3. SoldierUnit
   - 병사가 목표 위치까지 이동

전체 흐름

BarracksBuilding 생성
    ↓
SpawnSoldiers()
    ↓
SoldierUnit 생성
    ↓
병사들이 rallyPoint 위치로 이동

플레이어가 Barracks 클릭
    ↓
RallyController 활성
    ↓
우클릭 위치 지정
    ↓
병사들 새로운 위치로 이동
*/
public class BarracksBuilding : MonoBehaviour
{
    [Header("Soldier Spawn")]
    public SoldierUnit soldierPrefab;

    [Header("Rally Point")]
    // 병사들이 모일 위치
    public Vector2 rallyPoint;

    [Header("Soldier Formation Offsets")]
    /*
    병사들의 대형(formation) 위치

    예
    offsets =
    (0,0)
    (0.5,0)
    (-0.5,0)

    rallyPoint 기준으로 퍼져서 서게 됨
    */
    public Vector2[] soldierOffsets;
    // 생성된 병사 목록
    private List<SoldierUnit> spawnedSoldiers = new List<SoldierUnit>();
    
    /*
    Start()

    병영이 생성되면
    기본 rallyPoint 설정 후
    병사들을 생성
    */
    private void Start()
    {
        // 기본 집결지 위치
        rallyPoint = (Vector2)transform.position + new Vector2(0f, -1.2f);
        SpawnSoldiers();
    }
    
    /*
    OnMouseDown()

    병영을 클릭하면
    RallyController에게 선택되었다고 알림
    */
    private void OnMouseDown()
    {
        if (BarracksRallyController.Instance != null)
        {
            BarracksRallyController.Instance.SelectBarracks(this);
        }
    }

    /*
    SpawnSoldiers()

    역할
    - soldierOffsets 개수만큼 병사 생성
    - 병사에게 목표 위치 전달
    */
    public void SpawnSoldiers()
    {
        if (soldierPrefab == null)
        {
            Debug.LogError("Soldier Prefab이 연결되지 않았습니다.");
            return;
        }

        if (soldierOffsets == null || soldierOffsets.Length == 0)
        {
            Debug.LogError("Soldier Offsets가 설정되지 않았습니다.");
            return;
        }

        for (int i = 0; i < soldierOffsets.Length; i++)
        {
            // rallyPoint 기준으로 병사 목표 위치 계산
            Vector2 soldierTargetPos = rallyPoint + soldierOffsets[i];

            // 병사 생성
            SoldierUnit soldier = Instantiate(
                soldierPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );
            // 병사에게 목표 위치 전달
            soldier.Setup(soldierTargetPos);
            // 리스트에 저장
            spawnedSoldiers.Add(soldier);
        }
    }

    /*
    SetRallyPoint()

    플레이어가 새로운 집결지를 지정하면
    rallyPoint를 업데이트
    */
    public void SetRallyPoint(Vector2 newRallyPoint)
    {
        rallyPoint = newRallyPoint;
        UpdateSoldierPositions();
    }
    
    /*
    UpdateSoldierPositions()

    병사들에게 새로운 목표 위치 전달
    */
    private void UpdateSoldierPositions()
    {
        for (int i = 0; i < spawnedSoldiers.Count; i++)
        {
            if (spawnedSoldiers[i] == null)
                continue;

            if (i >= soldierOffsets.Length)
                continue;

            Vector2 newTargetPos = rallyPoint + soldierOffsets[i];
            spawnedSoldiers[i].SetTargetPosition(newTargetPos);
        }
    }
    /*
    OnDrawGizmosSelected()

    Scene 뷰에서

    파란색 : rallyPoint
    초록색 : 병사 위치

    디버깅용 시각화
    */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(rallyPoint, 0.12f);

        if (soldierOffsets != null)
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < soldierOffsets.Length; i++)
            {
                Vector2 pos = rallyPoint + soldierOffsets[i];
                Gizmos.DrawWireSphere(pos, 0.1f);
            }
        }
    }
}
