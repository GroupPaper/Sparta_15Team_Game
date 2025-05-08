using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private MovementController _movementController = new MovementController();
    private JumpController _jumpController = new JumpController();
    private GroundChecker _groundChecker = new GroundChecker();

    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private int maxJumpCount = 2;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }

        _movementController.Init(forwardSpeed, acceleration, maxSpeed);
        _jumpController.Init(jumpForce, maxJumpCount);
        _groundChecker.Init(_jumpController);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpController.TryJump();
        }
    }

    void FixedUpdate()
    {
        Vector2 newVelocity = _movementController.CalculateVelocity(_rigidbody.velocity);
        newVelocity = _jumpController.ApplyJump(newVelocity);
        _rigidbody.velocity = newVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _groundChecker.OnCollisionEnter2D(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _groundChecker.OnCollisionExit2D(collision);
    }
}
