# Unity Tower Defense Study

---

## Implemented Systems

### 1. Enemy Spawn

* `EnemySpawner`
* `Instantiate()` 기반 Enemy 생성
* Wave 시스템과 연동된 Spawn 구조

### 2. Path System

* Waypoint 기반 이동 경로
* Hierarchy child 자동 waypoint 등록
* `Gizmos` 경로 시각화

### 3. Enemy Movement & Combat

* `Vector2.MoveTowards` 이동
* 프레임 독립 이동 (`Time.deltaTime`)
* Enemy HP / 사망 처리
* Goal 도달 시 생명 감소
* Soldier와 근접 전투
* 블로킹 기반 전투 시스템

### 4. Tower Build System

* `TowerNode` 설치 위치 관리
* `BuildMenuUI` UI 기반 선택
* 클릭 → 선택 → 설치 흐름
* 골드 시스템 연동 (`GameManager`)
* 중복 설치 방지

### 5. Archer Tower System

* `ArcherTowerBuilding`
* 단일 타겟 공격
* 화살 Projectile 시스템

### 6. Mage Tower System 

* `MageTowerBuilding`
* `MageUnit`
* 범위 공격 (Splash Damage)
* `Physics2D.OverlapCircleAll` 기반 탐지
* Impact Effect 시스템

### 7. Targeting & Attack System

* 사거리 기반 Enemy 탐색
* 가장 가까운 Enemy 선택
* 공격 쿨타임 제어
* Coroutine 기반 공격 타이밍

### 8. Projectile & Impact System

* `ArrowProjectile`
* 포물선 이동 (`Vector2.Lerp`)
* Impact Effect 생성 및 자동 제거

### 9. Barracks System

* `BarracksBuilding`
* Soldier 유닛 생성
* Rally Point 시스템
* Formation 기반 배치
* 병사 사망 시 리스폰

### 10. Soldier Combat System 

* `SoldierUnit`
* HP 시스템
* Enemy 탐지 (`detectRange`)
* 근접 전투 (`attackRange`)
* Enemy 블로킹 시스템 (`SetBlocked / ReleaseBlock`)
* 1:1 전투 구조
* 공격 속도 기반 전투 (`attackRate`)
* 애니메이션 연동 (이동 / 공격)
* 병사 사망 → Barracks 리스폰 시스템

### 11. Spearman System 

* `SpearmanBuilding`
* Barracks 확장형 타워
* 동일 구조 기반 유닛 변형
* 타워 다양화 구조 설계

### 12. Wave System 

* `WaveManager`

* 버튼 기반 웨이브 시작

* 웨이브별 적 수 증가

* Spawn 간격 제어

* 자동 다음 웨이브 준비

* `WaveEnemyListener`

* Enemy 제거 감지

* 살아있는 적 수 추적

### 13. Game Management System

* `GameManager`
* 골드 시스템 (획득 / 소비)
* 생명 시스템
* Game Over 처리
* UI 연동 (Gold / Life / Wave)

### 14. UI System

* `TextMeshPro` UI
* Wave 버튼 시스템
* `UIButtonPulse` 애니메이션 효과
* Game Over UI

---

## Studying Concepts

이 프로젝트에서 공부하는 Unity / C# 개념

* Prefab & Instantiate
* MonoBehaviour lifecycle (`Awake`, `Start`, `Update`)
* Transform & Waypoint 시스템
* Vector 이동 (`MoveTowards`, `Lerp`)
* Coroutine (공격 / 웨이브 / 리스폰)
* Projectile & Splash Damage 시스템
* Physics2D (`OverlapCircle`)
* Wave 시스템 (게임 루프 핵심)
* Rally Point & Formation 시스템
* Singleton 패턴 (`GameManager`, Controller)
* 간단한 AI 구조 (Find → Move → Attack)
* Object 간 상호작용 (Enemy ↔ Soldier ↔ Tower)
* 전투 시스템 설계 (Blocking, 1:1 Combat)
* UI 시스템 (`TextMeshPro`, Button, Animation)
* Game State 관리 (Game Over)
