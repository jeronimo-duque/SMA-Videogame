using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovInputSystem : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [SerializeField] private FixedJoystick joystick;
    private CanvasGroup joystickCanvasGroup;

    private PlayerInput playerInputActions;

    public bool canMove = true;

    [Space(8)]
    [Header("Animations")]
    [SerializeField] private Animator animator;
    private Vector2 lastDirection = Vector2.down;

    void Awake()
    {
        playerInputActions = new PlayerInput();
        joystickCanvasGroup = joystick.GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        playerInputActions.Player.Move.performed += OnMovePerformed;
        playerInputActions.Player.Move.canceled += OnMoveCanceled;
        playerInputActions.Player.Enable();
    }

    void OnDisable()
    {
        playerInputActions.Player.Move.performed -= OnMovePerformed;
        playerInputActions.Player.Move.canceled -= OnMoveCanceled;
        playerInputActions.Player.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove)
        {
            moveInput = Vector2.zero;
            animator.SetFloat("MovX", lastDirection.x);
            animator.SetFloat("MovY", lastDirection.y);
            animator.SetBool("IsMove", false);
            return;
        }

        // Detecta la entrada del joystick
        Vector2 joystickInput = joystick != null ? new Vector2(joystick.Horizontal, joystick.Vertical) : Vector2.zero;

        // Si el joystick no tiene entrada, establece moveInput en cero
        if (joystick != null && Mathf.Abs(joystick.Horizontal) < 0.1f && Mathf.Abs(joystick.Vertical) < 0.1f)
        {
            moveInput = Vector2.zero;
        }
        else
        {
            moveInput = joystickInput.normalized;
            lastDirection = moveInput;
        }

        // Configura las animaciones de movimiento o idle
        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("MovX", moveInput.x);
            animator.SetFloat("MovY", moveInput.y);
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetFloat("MovX", lastDirection.x);
            animator.SetFloat("MovY", lastDirection.y);
            animator.SetBool("IsMove", false);
        }
    }

    void FixedUpdate()
    {
        // Solo mueve al jugador si moveInput tiene valores significativos
        if (canMove && moveInput != Vector2.zero)
        {
            rb.velocity = moveInput * speed;
        }
        else
        {
            rb.velocity = Vector2.zero; // Fuerza el Rigidbody2D a detenerse completamente
        }
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            moveInput = Vector2.zero;
            rb.velocity = Vector2.zero; // Detiene el Rigidbody2D completamente
        }
    }

    // Métodos para ocultar y mostrar el joystick
    public void HideJoystick()
    {
        if (joystickCanvasGroup != null)
        {
            joystickCanvasGroup.alpha = 0;  // Oculta el joystick
            joystickCanvasGroup.interactable = false;
        }
    }

    public void ShowJoystick()
    {
        if (joystickCanvasGroup != null)
        {
            joystickCanvasGroup.alpha = 1;  // Muestra el joystick
            joystickCanvasGroup.interactable = true;
        }
    }
}
