using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    Rigidbody2D rb;
    PlayerInput playerInput;
    Vector2 moveInput;
    public Vector2 moveDir { get ; private set; }
    public Vector2 LastDir { get; private set; }
    public bool isAttacking { get; private set; }

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
        if (isAttacking) //pour ne pas se déplacer quand le player attack
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        moveDir = moveInput * speed;
        rb.linearVelocity = moveDir;
        if (moveDir != Vector2.zero)
        {
            if (moveDir.x != 0) LastDir = new Vector2(Mathf.Sign(moveDir.x), 0);   // horizontal prioritaire
            else LastDir = new Vector2(0, Mathf.Sign(moveDir.y)); // vertical prioritaire
        }
    }

    public void GetAttack(bool _isAttacking)
    {
        isAttacking = _isAttacking;
    }

    // Input de direction
    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

}
