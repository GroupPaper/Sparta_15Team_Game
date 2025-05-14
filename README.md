# Sparta\_15Team\_Knight run

Unity 기반의 2D 게임 개발 프로젝트로, 팀원들과 협업하여 다양한 게임 기능을 구현하고 있습니다.

## 📌 프로젝트 개요

* **프로젝트명**: Sparta\_15Team\_Knight run
* **목표**: Unity를 활용한 2D 게임 개발 및 기능 구현
* **진행 방식**: GitHub를 통한 협업 및 버전 관리

## 🛠️ 주요 기능

* 플레이어 이동 및 점프 기능
* 적 캐릭터 AI 및 공격 패턴 구현
* 아이템 획득 및 인벤토리 시스템
* 게임 UI (체력바, 점수판 등)
* 레벨 디자인 및 씬 전환

## 📁 프로젝트 구조

```
Sparta_15Team_Game/Scripts
├── SoundManager.cs       # 싱글톤으로 BGM용·SFX용 AudioSource를 관리하며, PlayBGM/StopBGM/PlaySFX 호출로 사운드를 재생·제어
├── BGMPlayer.cs          # 씬이 시작될 때 SoundManager를 통해 지정된 BGM 클립을 자동으로 반복 재생
├── TimeManager.cs        # Time.deltaTime을 누적해 경과 시간을 분:초 포맷으로 UI에 표시하고, GetElapsedTime()으로 외부에서 조회할 수 있게 
├── ScoreManager.cs       # 경과 시간 기반 점수(timeScore)와 아이템 획득 점수(itemScore)를 분리해 누적·합산하며, PlayerPrefs로 최고점수를 저장·관리하고 UI에 갱신
├── HPBar.cs              # Slider로 체력바를 렌더링하고 설정된 속도로 HP를 자동 감소시키며, HP가 0이 되면 GameManager.GameOver()를 호출해 게임오버를 트리거
├── GameManager.cs        # ScoreManager와 TimeManager에서 최종 점수·시간을 가져와 GameOverUI에 전달하고, 게임오버 시 BGM 전환 등 전체 플로우를 관장
├── GameOverUI.cs         # 게임오버 패널의 활성화/비활성화를 관리하며, ShowGameOverPanel(score, time) 호출 시 텍스트 업데이트와 버튼 콜백(재시작·종료)을 처리
├── SceneManagement.cs    # Start/Main/Retry/Exit 등의 버튼 콜백으로 씬 전환과 애플리케이션 종료를 수행하고, 각 클릭 시 클릭 효과음을 재생
├── Item.cs               # 플레이어가 아이템과 충돌했을 때 Heal, Speed, Score 등 ItemEffectType에 맞는 효과를 Player에 적용하고 아이템을 파괴
├── FollowCamera.cs       # target 위치를 LateUpdate에서 offsetX만큼 X축으로, 고정된 Y값(fixedYValue)으로 보정해 카메라가 플레이어를 따라가도록 
├── GroundChecker.cs      # 플레이어 콜라이더 하단에 박스 체크(Physics2D.OverlapBox)를 실행해 바닥에 닿았는지 판정하고, IsGrounded()로 그 결과를 반환
├── JumpController.cs     # 최대 점프 횟수(maxJumpCount)와 현재 점프 횟수(currentJumpCount)를 관리하며, TryJump()으로 점프 시도를, ResetJump()으로 점프 상태 초기화를 처리
├── MovementController.cs # forwardSpeed와 acceleration을 기반으로 현재 이동 속도를 계산하고, 아이템 버프(ApplySpeedItemBuff)로 maxSpeed를 일정 시간 동안 증가시켰다 원복
├── Obstacle2.cs          # Player 태그의 오브젝트가 트리거에 진입하면 damage를 Player.TakeDamage()로 전달해 체력을 감소
└── Player.cs             # MovementController, JumpController, SlideController, GroundChecker를 조합해 캐릭터 이동·점프·슬라이드 로직을 실행하고, 해당 액션에 맞춰 SFX를 호출
```



## 👥 팀원

* **이영민**: 맵 제작
* **두승현**: 아이템 제작
* **류천복**: UI/UX 디자인 및 전체적인 씬 구현, 사운드
* **송진우**: 프로젝트 관리 및 맵 제작
* **김하빈**: 플레이어 제작

## 🛠️ 사용 기술

* **엔진**: Unity
* **언어**: C#
* **버전 관리**: Git & GitHub

## 📦 설치 및 실행 방법

1. 이 저장소를 클론합니다:

   ```bash
   git clone https://github.com/GroupPaper/Sparta_15Team_Game.git
   ```



2. Unity Hub를 열고, 클론한 프로젝트 폴더를 추가합니다.

3. 프로젝트를 열고, `Assets/Scenes` 폴더에서 메인 씬을 실행합니다.
