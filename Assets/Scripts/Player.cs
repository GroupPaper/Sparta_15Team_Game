using UnityEngine;

using System.Collections;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private MovementController _movementController = new MovementController();
    private JumpController _jumpController = new JumpController();
    private GroundChecker _groundChecker;
    private SlideController _slideController;
    private Animator jumpAnim;
    private Animator slideAnim;
    private bool isInvincible = false;
    public bool IsInvincible => isInvincible;

    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private int maxJumpCount = 2;

    private float verticalSpeed = 0f;

    [SerializeField] private GameObject slideObject;
    [SerializeField] private GameObject runObject;
    [SerializeField] private GameObject jumpObject;
    

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

    void Update()
    {
        float xSpeed = _movementController.GetCurrentSpeed();
        bool isGrounded = _groundChecker.IsGrounded();

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_jumpController.TryJump())
            {
                verticalSpeed = jumpForce;

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

        jumpAnim.SetFloat("VerticalSpeed", verticalSpeed);

        // 위치 이동
        transform.position += new Vector3(xSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0f);

        // 슬라이딩 입력 처리 (점프 중일 땐 슬라이딩 금지)
        if (!_jumpController.IsJumping())
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                runObject.SetActive(false);
                slideObject.SetActive(true);
                slideAnim.SetBool("isSliding", true);
                _slideController.TrySlide();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                slideAnim.SetBool("isSliding", false);
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
        StartCoroutine(EnableInvincibility(duration));
    }

    private IEnumerator EnableInvincibility(float duration)
    {
        isInvincible = true;

        gameObject.layer = LayerMask.NameToLayer("Invincible"); // 무적 상태로 레이어 마스크 변경

        yield return new WaitForSeconds(duration);

        gameObject.layer = LayerMask.NameToLayer("Player"); // 다시 원상태로 변경하여 충돌처리
        isInvincible = false;
    }

    public void AddScoreFromItem(int score)
    {
        scoreManager.AddItemScore(score);
    }
}
