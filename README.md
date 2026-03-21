# Unity Tower Defense Study

---

## Implemented Systems

### 1. Enemy Spawn

* `EnemySpawner`
* `Instantiate()`를 이용한 Enemy 생성
* Wave 시스템과 연동된 Spawn 구조

### 2. Path System

* Waypoint 기반 이동 경로
* Hierarchy child를 waypoint로 자동 등록
* `Gizmos`를 이용한 경로 시각화

### 3. Enemy Movement & Combat

* `Vector2.MoveTowards` 기반 이동
* `Time.deltaTime`을 이용한 프레임 독립 이동
* Enemy HP 시스템
* Damage / Death 처리
* Goal 도달 시 생명 감소
* Soldier와 근접 전투 시스템
* 공격 속도 기반 전투 로직

### 4. Tower Build System

* `TowerNode`를 이용한 설치 위치 관리
* `BuildMenuUI`를 통한 타워 선택 UI
* 클릭 → 메뉴 표시 → 타워 선택 → 설치 흐름
* `Instantiate()`로 타워 생성
* 골드 시스템 연동 (`GameManager`)
* 중복 설치 방지

### 5. Archer Tower System

* `ArcherTowerBuilding`
* 궁수 유닛 생성 구조

### 6. Targeting & Attack System

* `ArcherUnit`
* 사거리 내 Enemy 탐색
* 가장 가까운 Enemy 선택
* 공격 쿨타임 관리
* 공격 애니메이션

### 7. Projectile System

* `ArrowProjectile`
* 포물선 이동
* `Vector2.Lerp` 기반 궤적 계산
* Enemy 데미지 적용

### 8. Barracks System

* `BarracksBuilding`
* Soldier 유닛 생성
* Rally Point 시스템
* Formation 기반 배치
* 병사 사망 시 리스폰

### 9. Soldier Combat System

* `SoldierUnit`
* Enemy 탐지 및 블로킹
* 근접 전투 시스템
* Enemy와 1:1 전투 구조

### 10. Wave System 

* `WaveManager`

* 웨이브 기반 Enemy 생성

* 웨이브 증가 시 적 수 증가

* Spawn 간격 제어

* 다음 웨이브 자동 진행

* 모든 적 제거 시 다음 웨이브 시작

* `WaveEnemyListener`

* Enemy 사망 / 도착 감지

* 현재 살아있는 적 수 추적

### 11. Game Management System

* `GameManager`
* 골드 시스템 (획득 / 소비)
* 생명 시스템
* UI 업데이트 (Gold / Life / Wave)
* Game Over 처리
* 게임 상태 관리

---

## Studying Concepts

이 프로젝트에서 공부하는 Unity / C# 개념

* Prefab & Instantiate
* MonoBehaviour lifecycle (`Awake`, `Start`, `Update`)
* Transform & Waypoint 시스템
* Vector 이동 (`MoveTowards`, `Lerp`)
* Coroutine (`AttackRoutine`, Wave Spawn)
* Projectile 시스템
* Wave 시스템 (게임 루프 핵심)
* Rally Point 시스템
* Formation offsets (유닛 배치)
* Singleton 패턴 (`GameManager`, `RallyController`)
* 간단한 AI 구조 (Find → Move → Attack)
* Object 간 상호작용 (Enemy ↔ Soldier ↔ Tower)
* UI 시스템 (`TextMeshPro`, Panel 제어)
* Game State 관리 (Game Over)
