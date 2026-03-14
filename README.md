# Unity Tower Defense Study

Unity로 타워 디펜스 게임을 만들면서 시스템을 공부하는 저장소입니다.
각 시스템을 직접 구현하고 코드에 주석을 추가하여 구조와 동작 원리를 학습합니다.

---

## Implemented Systems

### 1. Enemy Spawn

* `EnemySpawner`
* `Instantiate()`를 이용한 Enemy 생성
* Enemy 생성 후 Path 전달

### 2. Path System

* Waypoint 기반 이동 경로
* Hierarchy child를 waypoint로 자동 등록
* `Gizmos`를 이용한 경로 시각화

### 3. Enemy Movement

* `Vector2.MoveTowards` 기반 이동
* `Time.deltaTime`을 이용한 프레임 독립 이동
* Enemy HP 시스템
* Damage / Death 처리
* Goal 도달 처리

### 4. Tower Build System

* `TowerNode`를 이용한 설치 위치 관리
* `BuildMenuUI`를 통한 타워 선택 UI
* 클릭 → 메뉴 표시 → 타워 선택 → 설치 흐름
* `Instantiate()`로 타워 생성
* 동일 위치 중복 설치 방지 (`currentTower`)

### 5. Archer Tower System

* `ArcherTowerBuilding`을 통한 궁수 유닛 생성
* 타워가 궁수 유닛을 보유하는 구조

### 6. Targeting & Attack System

* `ArcherUnit`
* 사거리 내 Enemy 탐색
* 가장 가까운 Enemy 선택
* 공격 쿨타임 관리
* 공격 애니메이션 실행

### 7. Projectile System

* `ArrowProjectile`
* 화살 포물선 이동
* `Vector2.Lerp` 기반 이동 계산
* 목표 Enemy에게 데미지 적용

---

## Studying Concepts

이 프로젝트에서 공부하는 Unity / C# 개념

* Prefab & Instantiate
* MonoBehaviour lifecycle (`Awake`, `Start`, `Update`)
* Transform & Waypoint 시스템
* Vector 이동 (`MoveTowards`, `Lerp`)
* Coroutine (`AttackRoutine`)
* Projectile 시스템
* UI 패널 제어 (`SetActive`)
* Object Reference 관리
* GameObject 생성 및 상태 관리
