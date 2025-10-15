using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Ground Check Settings")]
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => jumpPressed = true;
    }

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    private void Update()
    {
        isGrounded = CheckGrounded();
        UpdateAnimationStates();
    }

    private void FixedUpdate()
    {
        // Horizontal movement
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Jump
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpPressed = false;
    }

    private bool CheckGrounded()
    {
        Vector2 boxSize = new Vector2(col.bounds.size.x * 0.9f, col.bounds.size.y);
        RaycastHit2D hit = Physics2D.BoxCast(
            col.bounds.center,
            boxSize,
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        return hit.collider != null;
    }

    private void UpdateAnimationStates()
    {
        float horizontalSpeed = Mathf.Abs(moveInput.x);
        bool isJumping = !isGrounded;

        // Update animator parameters
        animator.SetFloat("Speed", horizontalSpeed);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsIdle", horizontalSpeed < 0.1f && !isJumping);

        // Flip sprite correctly (facing right when moving right)
        if (moveInput.x < 0.1f)
            spriteRenderer.flipX = false;  // Facing right
        else if (moveInput.x > -0.1f)
            spriteRenderer.flipX = true;   // Facing left
    }

    private void OnDrawGizmosSelected()
    {
        if (col == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            col.bounds.center + Vector3.down * groundCheckDistance,
            col.bounds.size
        );
    }
}
