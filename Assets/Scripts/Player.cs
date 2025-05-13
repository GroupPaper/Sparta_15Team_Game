using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementController _movementController; // 이동 컨트롤러
    private JumpController _jumpController = new JumpController(); // 점프 컨트롤러
    private GroundChecker _groundChecker; // 바닥 체크
    private SlideController _slideController; // 슬라이드 컨트롤러
    private Animator jumpAnim; // 점프 애니메이션
    [SerializeField] private float forwardSpeed = 3f; // 기본 속도
    [SerializeField] private float acceleration = 0.1f; // 초당 가속력
    [SerializeField] private float maxSpeed = 10f; // 현재 최고 속도

    [SerializeField] private float jumpForce = 6f; // 점프 힘
    [SerializeField] private float gravity = -20f; // 중력
    [SerializeField] private int maxJumpCount = 2; // 최대 점프 횟수
   
    private float verticalSpeed = 0f; // 수직 속도

    [SerializeField] private GameObject slideObject; // 슬라이드 오브젝트
    [SerializeField] private GameObject runObject; // 달리기 오브젝트
    [SerializeField] private GameObject jumpObject; // 점프 오브젝트
    [SerializeField] private GameObject hitObject; // 피격무적 오브젝트

    [SerializeField] private AudioClip footStepSfx; // 발소리 SFX
    [SerializeField] private AudioClip jumpSfx; // 점프 SFX
    [SerializeField] private AudioClip slideSfx; // 슬라이드 SFX

    private float footstepInterval = 0.4f; // 발소리 간격
    private float footstepTimer = 0f; // 발소리 타이머

    private bool isInvincible = false; // 무적 상태 여부
    private float invincibleDuration = 2f; // 무적 지속 시간 (초)
    private float invincibleTimer = 0f; // 타이머

    public bool IsInvincible() => isInvincible;

    // ApplyItemEffect 중개 메서드
    public HPBar currentHP;
    public ScoreManager scoreManager;

    void Start()
    {
        if (currentHP == null)
            currentHP = GetComponent<HPBar>();

        if (scoreManager == null)
            scoreManager = FindObjectOfType<ScoreManager>();

        _movementController = gameObject.AddComponent<MovementController>();
        _movementController.Init(forwardSpeed, acceleration, maxSpeed);
        
        _jumpController.Init(jumpForce, maxJumpCount);

        _slideController = gameObject.AddComponent<SlideController>();
        _slideController.Init(_jumpController);

        _groundChecker = GetComponent<GroundChecker>();
        _groundChecker.Init(_jumpController, _slideController, transform, this);

        jumpAnim = jumpObject.GetComponent<Animator>();

        runObject.SetActive(true);
        slideObject.SetActive(false);
        jumpObject.SetActive(false);
        hitObject.SetActive(false);

        invincibleTimer = 0f;
    }

    private bool wasSliding = false; // 슬라이드 상태를 저장하기 위한 변수

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer <= 0f)
            {
                // 무적 상태 종료
                isInvincible = false;
                hitObject.SetActive(false); // 무적 오브젝트 비활성화

                if (_jumpController.IsJumping())
                {
                    jumpObject.SetActive(true);
                    slideObject.SetActive(false);
                    runObject.SetActive(false);
                }
                else if (_slideController.IsSliding())
                {
                    slideObject.SetActive(true);
                    jumpObject.SetActive(false);
                    runObject.SetActive(false);
                }
                else
                {
                    runObject.SetActive(true);
                    jumpObject.SetActive(false);
                    slideObject.SetActive(false);
                }
            }
            if (!_jumpController.IsJumping())  // 점프 중이 아니라면
            {
                verticalSpeed = 0f;  // 수직 속도 초기화
            }
        }

        float xSpeed = _movementController.GetCurrentSpeed();
        bool isGrounded = _groundChecker.IsGrounded();

        if (isGrounded && xSpeed > 0f && !_slideController.IsSliding())
        {
            // 발소리 재생
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                SoundManager.Instance.PlaySFX(footStepSfx);
                footstepTimer = 0f;
            }
        }
        else
        {
            footstepTimer = footstepInterval; // 발소리 재생 안할 때는 리셋
        }

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_jumpController.TryJump())
            {
                verticalSpeed = jumpForce;
                SoundManager.Instance.PlaySFX(jumpSfx);

                if (_slideController.IsSliding() && !isInvincible)
                {
                    slideObject.SetActive(false);
                    _slideController.EndSlide();
                }

                if(!isInvincible)
                {
                    runObject.SetActive(false);
                    jumpObject.SetActive(true);
                    Debug.Log("점프 시 verticalSpeed: " + verticalSpeed);
                }
            }
        }

        // 중력 적용
        verticalSpeed += gravity * Time.deltaTime;

        // 수직 속도 리셋
        if (isGrounded && verticalSpeed < 0f)
        {
            verticalSpeed = 0f;
            _jumpController.ResetJump();
            
            if(!isInvincible)
            {
                // 점프 애니메이션 끄기
                jumpObject.SetActive(false);

                // 달리기/슬라이드 애니메이션은 상태에 따라
                if (_slideController.IsSliding())
                {
                    slideObject.SetActive(true);
                    runObject.SetActive(false);
                }
                else
                {
                    runObject.SetActive(true);
                    slideObject.SetActive(false);
                }
            }
        }

        if (jumpAnim != null && jumpObject.activeSelf && !isInvincible)
        {
            jumpAnim.SetFloat("VerticalSpeed", verticalSpeed);
        }

        // 위치 이동
        transform.position += new Vector3(xSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0f);

        bool isSliding = _slideController.IsSliding();

        if (isSliding && !wasSliding)
        {
            SoundManager.Instance.PlaySFX(slideSfx);
        }
        wasSliding = isSliding;

        // 슬라이딩 입력 처리 (점프 중일 땐 슬라이딩 금지)
        if (!_jumpController.IsJumping())
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if(!isInvincible)
                {
                    runObject.SetActive(false);
                    slideObject.SetActive(true);
                }
                _slideController.TrySlide();
                
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if(!isInvincible)
                {
                    slideObject.SetActive(false);
                    runObject.SetActive(true);
                }
                _slideController.EndSlide();
            }
        }
    }

    // ApplyItemEffect 중개 메서드
    public void HealFromItem(float amount)
    {
        currentHP.Heal(amount);
    }

    public void ApplySpeedBuffFromItem(float bonus, float duration)
    {
        _movementController.ApplySpeedItemBuff(bonus, duration);
    }

    public void AddScoreFromItem(int score)
    {
        scoreManager.AddItemScore(score);
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        if (currentHP != null)
            currentHP.Damage(damage);

        isInvincible = true;
        invincibleTimer = invincibleDuration;

        runObject.SetActive(false);
        jumpObject.SetActive(false);
        slideObject.SetActive(false);
        hitObject.SetActive(true);
    }
}