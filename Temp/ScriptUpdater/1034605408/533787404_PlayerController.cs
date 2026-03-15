using UnityEngine;

/// <summary>
/// A script that handles player movement.
/// In its current state it moves the player laterally and allows jumping.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private bool jumpPressed;

    private void Start()
    {
        Debug.Log("PlayerController Start Called"); // This will confirm if the Start method is called
        if (rb == null) return;
    }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        // Ensure the Rigidbody is in a correct initial state
        if (rb != null)
        {
            rb.simulated = true;  // Allow physics simulation when not frozen
            rb.gravityScale = 1f; // Restore gravity to normal when unfreezing
        }
    }

    void Update()
    {
        if (rb == null) return;

        Debug.Log("Current GameState: " + GameManager.Instance.CurrentGameState);  // Log current state

        switch (GameManager.Instance.CurrentGameState)
        {
            case GameState.InMenu:
                Debug.Log("InMenu State: Freezing Player");
                FreezePlayer();
                break;

            case GameState.InGame:
                Debug.Log("InGame State: Unfreezing Player");
                UnfreezePlayer();
                HandleMovement();
                break;

            case GameState.GameOver:
                Debug.Log("GameOver State: Freezing Player");
                FreezePlayer();
                break;
        }

        jumpPressed = false;
    }

    private void FreezePlayer()
    {
        Debug.Log("Freezing Player.");

        // Stop velocity using linearVelocity
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Log the gravity scale for debugging
        Debug.Log("Gravity Scale Before Freezing: " + rb.gravityScale);

        rb.gravityScale = 0f;  // Disable gravity
        rb.simulated = false;  // Disable physics simulation

        // Log the gravity scale again after freezing
        Debug.Log("Gravity Scale After Freezing: " + rb.gravityScale);

        // Freeze the entire game world
        Time.timeScale = 0f;  // Freezes everything
    }

    private void UnfreezePlayer()
    {
        Debug.Log("Unfreezing Player.");

        // Reinitialize Rigidbody2D properties to ensure gravityScale is set properly
        rb.linearVelocity = Vector2.zero;       // Stop velocity
        rb.angularVelocity = 0f;          // Stop rotation
        rb.gravityScale = 1f;             // Restore gravity
        rb.simulated = true;              // Enable physics simulation

        // Log current gravity scale
        Debug.Log("Gravity scale after unfreezing: " + rb.gravityScale);

        Time.timeScale = 1f;  // Unfreeze game world
    }

    private void HandleMovement()
    {
        // Move forward constantly
        float distancePerFrame = speed * Time.deltaTime;
        transform.Translate(distancePerFrame, 0, 0);

        // Handle jump input
        if (jumpPressed && IsGrounded())
        {
            Jump();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
    }

    private void Jump()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0;
        rb.linearVelocity = velocity;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Called automatically by PlayerInput when the jump action is triggered
    /// </summary>
    public void OnJump()
    {
        jumpPressed = true;
    }

    public void Reset()
    {
        rb.linearVelocity = Vector2.zero;

        transform.position = startPosition;
        transform.rotation = Quaternion.identity;

        rb.simulated = false;
        jumpPressed = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.down * groundCheckDistance
        );
    }
}