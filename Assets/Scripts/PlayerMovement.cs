/*using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isPickingUp = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Si no se asign� groundCheck, cr�alo
        if (groundCheck == null)
        {
            GameObject check = new GameObject("GroundCheck");
            check.transform.SetParent(transform);
            check.transform.localPosition = new Vector3(0, 0.1f, 0);
            groundCheck = check.transform;
        }
    }

    void Update()
    {
        if (isPickingUp) return; // No moverse durante animaci�n de recogida

        GroundCheck();
        Move();
        Jump();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Peque�a fuerza hacia abajo
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calcular rotaci�n
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0f, targetAngle, 0f),
                Time.deltaTime * rotationSpeed
            );

            // Determinar velocidad (correr o caminar)
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = isRunning ? runSpeed : walkSpeed;

            // Mover
            controller.Move(direction * currentSpeed * Time.deltaTime);

            // Actualizar animaci�n
            animator.SetFloat("Speed", currentSpeed);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }
    }

    public void PlayPickUpAnimation()
    {
        isPickingUp = true;
        animator.SetTrigger("PickUp");
        Invoke(nameof(EndPickUpAnimation), 1.5f); // Duraci�n aproximada de la animaci�n
    }

    void EndPickUpAnimation()
    {
        isPickingUp = false;
    }
}*/

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float fuerzaSalto = 5f;

    [Header("Detecci�n de Suelo")]
    [SerializeField] private Transform checkSuelo;
    [SerializeField] private float radioCheckSuelo = 0.2f;
    [SerializeField] private LayerMask capaSuelo;

    private Rigidbody rb;
    private bool enSuelo;
    private float movimientoHorizontal;
    private float movimientoVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Capturar input de movimiento
        movimientoHorizontal = Input.GetAxis("Horizontal"); // A y D
        movimientoVertical = Input.GetAxis("Vertical");     // W y S

        // Verificar si est� en el suelo
        enSuelo = Physics.CheckSphere(checkSuelo.position, radioCheckSuelo, capaSuelo);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            Saltar();
        }
    }

    void FixedUpdate()
    {
        // Mover el jugador
        Mover();
    }

    void Mover()
    {
        Vector3 movimiento = transform.right * movimientoHorizontal + transform.forward * movimientoVertical;
        movimiento = movimiento.normalized * velocidadMovimiento;

        // Mantener la velocidad vertical (gravedad)
        movimiento.y = rb.linearVelocity.y;

        rb.linearVelocity = movimiento;
    }

    void Saltar()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
    }

    // Visualizar el �rea de detecci�n de suelo en el editor
    void OnDrawGizmosSelected()
    {
        if (checkSuelo != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(checkSuelo.position, radioCheckSuelo);
        }
    }
}