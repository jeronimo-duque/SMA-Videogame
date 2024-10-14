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

        if (moveInput != Vector2.zero)
        {
            moveInput.Normalize();
            lastDirection = moveInput.normalized;
        }
        else
        {
            // Put the idle animation with player orientation.
            animator.SetFloat("MovX", lastDirection.x);
            animator.SetFloat("MovY", lastDirection.y);
            animator.SetBool("IsMove", false);
        }
    }

    void FixedUpdate()
    {
        moveVelocity = moveInput * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

 

        // Update the animator params to movement.
        animator.SetFloat("MovX", moveInput.x);
        animator.SetFloat("MovY", moveInput.y);

        animator.SetBool("IsMove", moveInput.magnitude > 0);

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
