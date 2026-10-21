using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    Rigidbody2D rb;
    PlayerInput playerInput;
    Vector2 moveInput;
    public Vector2 moveDir { get ; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        // Désactive les actions de base et active les actions par défaut
        InputSystem.actions.Disable();
        playerInput.currentActionMap?.Enable();
    }

    void FixedUpdate()
    {
        moveDir = moveInput * speed;
        rb.linearVelocity = moveDir;
    }

    // Input de direction
    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
}
