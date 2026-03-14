using UnityEngine;
/*


이 파일은 다음 세 가지 스크립트를 공부하기 위해
주석을 추가한 버전입니다.

1. ArcherTowerBuilding
   - 타워가 생성되면 궁수 유닛을 스폰

2. ArcherUnit
   - 사거리 내 적 탐지
   - 공격 애니메이션 실행
   - 화살 발사

3. ArrowProjectile
   - 포물선 궤적으로 이동
   - 적에게 데미지 적용

전체 게임 흐름

Tower 설치
   ↓
ArcherTowerBuilding
   ↓ SpawnArcher()
ArcherUnit 생성
   ↓
적 탐지
   ↓
공격 애니메이션
   ↓
ArrowProjectile 발사
   ↓
Enemy.TakeDamage()
*/
public class ArcherTowerBuilding : MonoBehaviour
{
    [Header("Archer Spawn")]
    public ArcherUnit archerUnitPrefab;
    public Transform archerSpawnPoint;

    private ArcherUnit currentArcher;

    /*
    Start()

    타워가 생성되면 자동으로 궁수를 하나 생성
    */
    private void Start()
    {
        SpawnArcher();
    }
    /*
    SpawnArcher()

    역할
    - 궁수 프리팹을 생성
    - spawnPoint 위치에 배치
    - 이미 궁수가 있으면 생성하지 않음
    */
    public void SpawnArcher()
    {
        if (archerUnitPrefab == null)
        {
            Debug.LogError("ArcherUnit 프리팹이 연결되지 않았습니다.");
            return;
        }

        if (archerSpawnPoint == null)
        {
            Debug.LogError("Archer Spawn Point가 없습니다.");
            return;
        }

        if (currentArcher != null)
            return;

        currentArcher = Instantiate(archerUnitPrefab, archerSpawnPoint.position, Quaternion.identity, transform);
    }
}
