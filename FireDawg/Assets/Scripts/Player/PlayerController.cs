using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Ground Check Settings")]
    [Tooltip("How far below the collider to check for ground.")]
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D col;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        inputActions = new PlayerInputActions();

        // Input bindings
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => jumpPressed = true;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        isGrounded = CheckGrounded();
    }

    private void FixedUpdate()
    {
        // Move horizontally
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Jump only if grounded
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpPressed = false;
    }

    private bool CheckGrounded()
    {
        // Cast a box below the player collider
        RaycastHit2D hit = Physics2D.BoxCast(
            col.bounds.center,
            col.bounds.size,
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        return hit.collider != null;
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