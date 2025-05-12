using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    private MovementController _movementController = new MovementController();
    private JumpController _jumpController = new JumpController();
    private GroundChecker _groundChecker = new GroundChecker();
    private SlideController _slideController = new SlideController();

    private bool isJumping = false;
    private bool isInvincible = false;
    public bool IsInvincible => isInvincible;

    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private int maxJumpCount = 2;

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
        _groundChecker.Init(_jumpController);
        _slideController.Init(_jumpController);
    }

    void Update()
    {
        float xSpeed = _movementController.GetCurrentSpeed();
        bool isGrounded = _groundChecker.IsGrounded(); // 바닥 여부 확인

        float yDisplacement = _jumpController.GetJumpDisplacement() * Time.deltaTime;
        transform.position += new Vector3(xSpeed * Time.deltaTime, yDisplacement, 0f);

        if (Input.GetKeyDown(KeyCode.Space)) // 슬라이딩 중에도 점프 가능
        {
            isJumping = true;

            // 슬라이딩 중이라면 슬라이딩을 취소하고 점프 시작
            if (_slideController.IsSliding())
            {
                slideObject.SetActive(false);
                Debug.Log("슬라이드 취소");
                _slideController.EndSlide();
            }

            runObject.SetActive(false);
            jumpObject.SetActive(true);
            Debug.Log("점프");
            _jumpController.TryJump();
        }

        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_jumpController.IsJumping()) // 점프 중에는 슬라이딩 시작 안됨
            {
                runObject.SetActive(false);
                slideObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.LeftShift) && !_jumpController.IsJumping()) // 슬라이딩 중일 때 슬라이딩 액션
            {
                _slideController.TrySlide();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift)) // 쉬프트 떼면 슬라이딩 종료
            {
                slideObject.SetActive(false);
                runObject.SetActive(true);
                _slideController.EndSlide(); // 슬라이딩 종료
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
