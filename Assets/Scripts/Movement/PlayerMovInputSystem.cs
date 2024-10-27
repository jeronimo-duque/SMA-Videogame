using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovInputSystem : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Vector2 moveInput;

    // Referencia al InputAction
    private PlayerInput playerInputActions;

    public bool canMove = true;

    [Space(8)]
    [Header("Animations")]
    [SerializeField] private Animator animator;
    private Vector2 lastDirection = Vector2.down;

    void Awake()
    {
        playerInputActions = new PlayerInput();
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
            // Si no puede moverse, se asegura de que el input y las animaciones estén detenidas
            moveInput = Vector2.zero;
            animator.SetFloat("MovX", lastDirection.x);
            animator.SetFloat("MovY", lastDirection.y);
            animator.SetBool("IsMove", false);
            return;
        }

        if (moveInput != Vector2.zero)
        {
            moveInput.Normalize();
            lastDirection = moveInput.normalized;
        }
        else
        {
            // Animación de idle con orientación
            animator.SetFloat("MovX", lastDirection.x);
            animator.SetFloat("MovY", lastDirection.y);
            animator.SetBool("IsMove", false);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            moveVelocity = moveInput * speed;
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

            // Actualizar los parámetros del animador para el movimiento
            animator.SetFloat("MovX", moveInput.x);
            animator.SetFloat("MovY", moveInput.y);
            animator.SetBool("IsMove", moveInput.magnitude > 0);
        }
        else
        {
            // Si no puede moverse, detener el movimiento del Rigidbody
            rb.velocity = Vector2.zero;
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
        }
    }
}
