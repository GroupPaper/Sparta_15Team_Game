using UnityEngine;

using System.Collections;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private MovementController _movementController = new MovementController(); // 이동 컨트롤러
    private JumpController _jumpController = new JumpController(); // 점프 컨트롤러
    private GroundChecker _groundChecker; // 바닥 체크
    private SlideController _slideController; // 슬라이드 컨트롤러
    private Animator jumpAnim; // 점프 애니메이션
    private Animator slideAnim; // 슬라이드 애니메이션
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

    [SerializeField] private AudioClip footStepSfx; // 발소리 SFX
    [SerializeField] private AudioClip jumpSfx; // 점프 SFX
    [SerializeField] private AudioClip slideSfx; // 슬라이드 SFX

    private float footstepInterval = 0.4f; // 발소리 간격
    private float footstepTimer = 0f; // 발소리 타이머

    // ApplyItemEffect 중개 메서드
    public HPBar currentHP;
    public MovementController movementController;
    public ScoreManager scoreManager;

    void Start()
    {
        _movementController.Init(forwardSpeed, acceleration, maxSpeed);
        _jumpController.Init(jumpForce, maxJumpCount);

        _slideController = gameObject.AddComponent<SlideController>();
        _slideController.Init(_jumpController);

        _groundChecker = GetComponent<GroundChecker>();
        _groundChecker.Init(_jumpController, _slideController, transform);

        jumpAnim = jumpObject.GetComponent<Animator>();
        slideAnim = slideObject.GetComponent<Animator>();
    }

    private bool wasSliding = false; // 슬라이드 상태를 저장하기 위한 변수

    void Update()
    {
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

                if (_slideController.IsSliding())
                {
                    slideObject.SetActive(false);
                    _slideController.EndSlide();
                }

                runObject.SetActive(false);
                jumpObject.SetActive(true);
                Debug.Log("점프 시 verticalSpeed: " + verticalSpeed);
            }
        }

        // 중력 적용
        verticalSpeed += gravity * Time.deltaTime;

        // 수직 속도 리셋
        if (isGrounded && verticalSpeed < 0f)
        {
            verticalSpeed = 0f;
            _jumpController.ResetJump();
            
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

        if (jumpAnim != null && jumpObject.activeSelf)
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
                runObject.SetActive(false);
                slideObject.SetActive(true);
                _slideController.TrySlide();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                slideObject.SetActive(false);
                runObject.SetActive(true);
                _slideController.EndSlide();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("플레이어 충돌 발생: " + collision.gameObject.name);
        _groundChecker.OnCollisionEnter2D(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _groundChecker.OnCollisionExit2D(collision);
    }

    // ApplyItemEffect 중개 메서드
    public void HealFromItem(float amount)
    {
        currentHP.Heal(amount);
    }

    public void ApplySpeedBuffFromItem(float bonus, float duration)
    {
        movementController.ApplySpeedItemBuff(bonus, duration);
    }

    public void AddScoreFromItem(int score)
    {
        scoreManager.AddItemScore(score);
    }

    public void TakeDamage(int damage)
    {
        if (currentHP != null)
            currentHP.Damege(damage);
    }
}
