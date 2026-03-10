using UnityEngine;
/*
역할
- 게임 시작 또는 특정 시점에 Enemy를 생성
- 생성된 Enemy에게 이동 경로(Path)를 전달

타워 디펜스 구조에서 위치

EnemySpawner
   ↓ Instantiate
Enemy
   ↓ Setup(Path)
Path

이 코드에서 공부할 핵심 개념
1. Instantiate() : 게임 오브젝트 생성
2. Prefab : 미리 만들어둔 오브젝트 템플릿
3. 의존성 전달 (Dependency Injection)
4. Start() : 게임 시작 시 실행되는 함수
5. Unity 컴포넌트 참조

게임만들라면 이건 확실히 하는게 좋음 안쓰는곳이 어딧노

게임에서는
- 웨이브 시스템
- 여러 Enemy 생성
- 시간 간격 Spawn
추가 예정
*/

public class EnemySpawner : MonoBehaviour
{
    //프리펩 저장
    public Enemy enemyPrefab;
    /*
    Spawn된 Enemy에게
    Setup()을 통해 전달됨
    */
    public Path path;

    private void Start()
    {
        SpawnEnemy();
    }
    /*
    Enemy 생성 함수

    흐름
    1. enemyPrefab 복제
    2. Enemy 컴포넌트 가져오기
    3. Path 전달
    */
    public void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(enemyPrefab);
        newEnemy.Setup(path);
    }

}
