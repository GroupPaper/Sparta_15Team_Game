using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementController _movementController = new MovementController();
    private JumpController _jumpController = new JumpController();
    private GroundChecker _groundChecker;
    private SlideController _slideController = new SlideController();

    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private int maxJumpCount = 2;

    [SerializeField] private GameObject slideObject;
    [SerializeField] private GameObject runObject;
    [SerializeField] private GameObject jumpObject;

    private bool isJumping = false;

    void Start()
    {
        _movementController.Init(forwardSpeed, acceleration, maxSpeed);
        _jumpController.Init(jumpForce, maxJumpCount);
        // _groundChecker.Init(_jumpController);
        _slideController.Init(_jumpController);

        _groundChecker = new GroundChecker();
        _groundChecker.Init(_jumpController);
    }

    void Update()
    {
        float xSpeed = _movementController.GetCurrentSpeed();
        bool isGrounded = _groundChecker.IsGrounded(); // 바닥 여부 확인
        // _jumpController.ApplyGravity(isGrounded); // 중력 적용

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

        // 점프 중이 아닐 때만 슬라이딩 가능
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
        if (isJumping)
        {
            _groundChecker.OnCollisionEnter2D(collision);
            runObject.SetActive(true);
            jumpObject.SetActive(false);
            isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _groundChecker.OnCollisionExit2D(collision);
    }
}

// public class Player : MonoBehaviour
// {
//     private MovementController _movementController = new MovementController();
//     private JumpController _jumpController = new JumpController();
//     private GroundChecker _groundChecker;
//     private SlideController _slideController = new SlideController();

//     [SerializeField] private float forwardSpeed = 3f;
//     [SerializeField] private float acceleration = 0.1f;
//     [SerializeField] private float maxSpeed = 10f;

//     [SerializeField] private float jumpForce = 6f;
//     [SerializeField] private int maxJumpCount = 2;

//     [SerializeField] private GameObject slideObject;
//     [SerializeField] private GameObject runObject;
//     [SerializeField] private GameObject jumpObject;

//     private bool isJumping = false;

//     void Start()
//     {
//         _movementController.Init(forwardSpeed, acceleration, maxSpeed);
//         _jumpController.Init(jumpForce, maxJumpCount);

//         if (_groundChecker == null) {
//             Debug.LogError("GroundChecker 컴포넌트가 Player 객체에 할당되지 않았습니다!");
//         }
//         else {
//             _groundChecker.Init(_jumpController); // 정상적으로 초기화된 경우에만 호출
//         }

//         _slideController.Init(_jumpController);
//     }

//     void Update()
//     {
//         float xSpeed = _movementController.GetCurrentSpeed();
//         bool isGrounded = _groundChecker.IsGrounded(); // 바닥 여부 확인
//         float yDisplacement = _jumpController.GetJumpDisplacement() * Time.deltaTime;
//         transform.position += new Vector3(xSpeed * Time.deltaTime, yDisplacement, 0f);

//         if (Input.GetKeyDown(KeyCode.Space)) 
//         {
//             isJumping = true;
//             if (_slideController.IsSliding())
//             {
//                 slideObject.SetActive(false);
//                 _slideController.EndSlide();
//             }

//             runObject.SetActive(false);
//             jumpObject.SetActive(true);
//             _jumpController.TryJump();
//         }

//         if (!isJumping)
//         {
//             if (Input.GetKeyDown(KeyCode.LeftShift) && !_jumpController.IsJumping())
//             {
//                 runObject.SetActive(false);
//                 slideObject.SetActive(true);
//             }

//             if (Input.GetKey(KeyCode.LeftShift) && !_jumpController.IsJumping())
//             {
//                 _slideController.TrySlide();
//             }

//             if (Input.GetKeyUp(KeyCode.LeftShift))
//             {
//                 slideObject.SetActive(false);
//                 runObject.SetActive(true);
//                 _slideController.EndSlide();
//             }
//         }
//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (isJumping)
//         {
//             _groundChecker.OnCollisionEnter2D(collision);
//             runObject.SetActive(true);
//             jumpObject.SetActive(false);
//             isJumping = false;
//         }
//     }

//     void OnCollisionExit2D(Collision2D collision)
//     {
//         _groundChecker.OnCollisionExit2D(collision);
//     }
// }
