using UnityEngine;

/// <summary>
/// A script that handles player movement.
/// In it's currwnt state it only moves the player laterally in the positive x.
/// </summary>

public class PlayerController : MonoBehaviour
{
    // Edit for source control
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float groundCheckDistance = 0.1f;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private bool jumpPressed;

    public void Initialize()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) return;

        // Calculate the distance we should move the object on a per frame basis. -> Make it frame rate independent.
        float distancePerFrame = speed * Time.deltaTime;

        // Move the attached game object in the x coordinate.
        transform.Translate(distancePerFrame, 0, 0);
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
    }


    private void Jump()
    {
        // Reset vertical speed for consistent jump height
        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0;
        rb.linearVelocity = velocity;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Called automatically by PlayerInput (SendMessages) when the jump action is triggared
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
        // Visualize the ground check in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.down * groundCheckDistance
        );
    }
}
