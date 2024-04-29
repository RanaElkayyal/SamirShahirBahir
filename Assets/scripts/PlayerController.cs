using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action OnJump;
    public event Action<bool> OnGroundChange;



    [Header("Movement settings")]
    [SerializeField] float speed;

    [Header("Jumps settings")]
    [SerializeField] float jumpPower;
    [SerializeField] int maxjumpCount;
    [SerializeField] int endJumpModifier;


    [Header("Unity Components")]
    [SerializeField] Rigidbody2D rb;

    [Header("Input settings")]
    [SerializeField] InputActionProperty moveInput;
    [SerializeField] InputActionProperty jumpInput;

    [Header("COLLISION SETTINGS")]
    [SerializeField] Vector3 groundOffset;
    [SerializeField] Vector3 cellingOffset;

    [SerializeField] float rad;
    [SerializeField] float distance = 0.05f;
    [SerializeField] LayerMask collisoinLayer;

    [Header("GRAVITY SETTINGS")]
    [SerializeField] float groundForce;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float fallAccleration;

    [SerializeField] MeatCollector meatCollector;

    public bool IsUsingAbility { get; set; }


    Vector2 velocity;
    bool isGrounded;
    bool isJumping;
    bool endJumpEarly;
    bool canUlt;
    int jumpsLeft;


    int lastPlayerDirection = 1;
    private MovingPlatform _platform;


    private void OnEnable()
    {
        jumpInput.action.performed += OnJumpEvent;
        jumpInput.action.Enable();
        moveInput.action.Enable();

        meatCollector.OnCollectMaxMeatnumb += () => canUlt = true;
    }


    private void OnDisable()
    {
        moveInput.action.Disable();
        jumpInput.action.Disable();
        jumpInput.action.performed -= OnJumpEvent;

        meatCollector.OnCollectMaxMeatnumb -= () => canUlt = true;

    }


    private void FixedUpdate()
    {
        HandleCollision();
        Jump();
        MovePlayer();
        HandleGravity();
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        if (_platform != null)
        {
            rb.velocity = velocity + _platform.GetCurrentSpeed();
        }
        else
        {
            rb.velocity = velocity;
        }
    }

    void Jump()
    {
        if (isJumping)
        {
            if (jumpsLeft > 0)
            {
                velocity.y = jumpPower; ;
                jumpsLeft = jumpsLeft - 1;
                OnJump?.Invoke();
            }
            isJumping = false;

        }
    }


    void MovePlayer()
    {
        float movementDirection = moveInput.action.ReadValue<Vector2>().x;
        if (movementDirection < 0)
        {
            lastPlayerDirection = -1;
        }
        else if (movementDirection > 0)
        {
            lastPlayerDirection = 1;

        }

        velocity.x = movementDirection * speed;

    }

    void HandleCollision()
    {

        bool groundHit = Physics2D.CircleCast(transform.position + groundOffset, rad,
            Vector3.down, distance, collisoinLayer);

        bool cellingHit = Physics2D.CircleCast(transform.position + cellingOffset, rad,
            Vector3.up, distance, collisoinLayer);

        if (cellingHit)
        {
            velocity.y = 0;
            endJumpEarly = true;

        }
        if (groundHit && !isGrounded)
        {
            //laned on ground 
            isGrounded = true;
            jumpsLeft = maxjumpCount;
            endJumpEarly = false;
            OnGroundChange?.Invoke(true);

        }

        if (isGrounded && !groundHit)
        {
            // left ground
            isGrounded = false;
            OnGroundChange?.Invoke(false);

        }

    }

    private void HandleGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -groundForce;
        }
        else
        {
            float fallSpeed = maxFallSpeed;
            if (endJumpEarly)
            {
                fallSpeed = fallSpeed * endJumpModifier;
            }

            velocity.y = Mathf.MoveTowards(velocity.y, -fallSpeed, fallAccleration * Time.deltaTime);
        }
    }

    public void UseUltimate()
    {
        canUlt = false;
        meatCollector.SpendMeat();
    }

    private void OnJumpEvent(InputAction.CallbackContext obj)
    {
        isJumping = true;
    }

    public int GetLastPlayerDirection()
    {
        return lastPlayerDirection;
    }
    public float GetHorizontalInput()
    {
        return Mathf.Abs(moveInput.action.ReadValue<Vector2>().x);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool CanUseUlt()
    {
        return canUlt;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position + groundOffset, rad);
        Gizmos.DrawSphere(transform.position + cellingOffset, rad);
    }
#endif
    public void SetPlatForm(MovingPlatform movingPlatform)
    {
        _platform = movingPlatform;
    }
}
