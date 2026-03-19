# Unity Tower Defense Study
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

### 3. Enemy Movement & Combat

* `Vector2.MoveTowards` 기반 이동
* `Time.deltaTime`을 이용한 프레임 독립 이동
* Enemy HP 시스템
* Damage / Death 처리
* Goal 도달 시 생명 감소
* Soldier와 근접 전투 시스템
* 공격 속도 (`attackRate`) 기반 공격
* 공격 애니메이션 및 타이밍 처리

### 4. Tower Build System

* `TowerNode`를 이용한 설치 위치 관리
* `BuildMenuUI`를 통한 타워 선택 UI
* 클릭 → 메뉴 표시 → 타워 선택 → 설치 흐름
* `Instantiate()`로 타워 생성
* 동일 위치 중복 설치 방지 (`currentTower`)
* 골드 시스템 연동 (`GameManager`)

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

### 8. Barracks System

* `BarracksBuilding`
* 병사(Soldier) 유닛 생성
* Rally Point(집결지) 시스템
* 병사 대형(Formation) 위치 관리
* 병사 이동 (`MoveTowards`)
* 병사 사망 시 일정 시간 후 리스폰
* Formation 슬롯 기반 병사 관리

### 9. Soldier Combat System

* `SoldierUnit`
* Enemy 탐지 (`detectRange`)
* 근접 전투 (`attackRange`)
* Enemy 블로킹 시스템 (`SetBlocked`)
* Enemy와 1:1 전투 구조
* 공격 속도 기반 데미지 처리
* 병사 사망 → Barracks에 알림

### 10. Game Management System

* `GameManager`
* 골드 관리 (획득 / 소비)
* 타워 건설 비용 처리
* Enemy 처치 시 골드 보상
* 생명 시스템 (Goal 도달 시 감소)
* Game Over 처리

---

## Studying Concepts

이 프로젝트에서 공부하는 Unity / C# 개념

* Prefab & Instantiate
* MonoBehaviour lifecycle (`Awake`, `Start`, `Update`)
* Transform & Waypoint 시스템
* Vector 이동 (`MoveTowards`, `Lerp`)
* Coroutine (`AttackRoutine`, Respawn)
* Projectile 시스템
* Rally Point 시스템
* Formation offsets (유닛 대형 시스템)
* Singleton 패턴 (`GameManager`, `RallyController`)
* 간단한 AI 구조 (Find → Move → Attack)
* Object 간 상호작용 (Enemy ↔ Soldier ↔ Barracks)
* UI 패널 제어 (`SetActive`)
* Object Reference 관리
* GameObject 생성 및 상태 관리

---
