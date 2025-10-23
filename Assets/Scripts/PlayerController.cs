using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float force = 30f;
    [SerializeField] private float upForce = 250f;

    [Header("Control Type")]
    [SerializeField] private bool useCharacterController = true;

    // Components
    private Animator animator;
    private CharacterController character;
    private Rigidbody rb;
    private PlayerInput inputPlayer;

    // Input
    private Vector2 moveInput;
    private Vector3 inicio;

    private void Awake()
    {
        // Initialize Animator
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Initialize CharacterController
        if (character == null)
        {
            character = GetComponent<CharacterController>();
        }

        // Initialize Rigidbody
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        // Initialize PlayerInput
        if (inputPlayer == null)
        {
            inputPlayer = GetComponent<PlayerInput>();
        }
    }

    void Start()
    {
        inicio = transform.position;
    }

    void Update()
    {
        // Read input from PlayerInput if available
        if (inputPlayer != null)
        {
            moveInput = inputPlayer.actions["Move"].ReadValue<Vector2>();
        }

        // Reset if falling
        if (transform.position.y < -2f)
        {
            Reset();
        }

        // Action Map switching
        if (inputPlayer != null)
        {
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                inputPlayer.SwitchCurrentActionMap("Manejar");
                Debug.Log("accion map manejar");
            }
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                inputPlayer.SwitchCurrentActionMap("Player");
                Debug.Log("accion map player");
            }
        }

        // Movement with CharacterController (only if using CharacterController mode)
        if (useCharacterController && character != null)
        {
            Vector3 movimiento = new Vector3(moveInput.x, 0, moveInput.y).normalized * speed * Time.deltaTime;
            character.Move(movimiento);
        }

        // Update animator parameters
        if (animator != null)
        {
            animator.SetFloat("x", moveInput.x);
            animator.SetFloat("y", moveInput.y);
        }
    }

    void FixedUpdate()
    {
        // Movement with Rigidbody (only if not using CharacterController mode)
        if (!useCharacterController && rb != null)
        {
            rb.AddForce(new Vector3(moveInput.x, 0f, moveInput.y) * force);
        }
    }

    // Input System callback for movement (alternative input method)
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
    }

    // Jump functionality
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && rb != null)
        {
            rb.AddForce(Vector3.up * upForce);
            Debug.Log(callbackContext.phase);
        }
    }

    // Reset player to initial position
    private void Reset()
    {
        transform.position = inicio;
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Driving functionality
    public void Manejar(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("manejando el auto");
        }
    }
}