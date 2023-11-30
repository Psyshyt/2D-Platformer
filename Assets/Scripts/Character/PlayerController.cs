using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f; // Множитель скорости при беге
    public float jumpForce = 10f;
    public float airControlFactor = 0.5f; // Фактор управления в воздухе (от 0 до 1)

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        // Получаем компонент Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Управление перемещением
        float horizontalInput = Input.GetAxis("Horizontal");

        // Если персонаж находится на земле, разрешаем перемещение и бег
        if (isGrounded)
        {
            Move(horizontalInput);

            // Управление бегом
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sprint();
            }

            // Управление прыжком
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            // Применяем управление в воздухе
            AirControl(horizontalInput);

            // Применяем торможение в воздухе
            ApplyBrakes(airControlFactor);
        }

        // Обновляем параметры аниматора
        UpdateAnimatorParameters(horizontalInput);
    }

    void Move(float horizontalInput)
    {
        Vector2 moveDirection = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    void Sprint()
    {
        // Умножаем скорость на множитель при беге
        rb.velocity = new Vector2(rb.velocity.x * sprintMultiplier, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void AirControl(float horizontalInput)
    {
        // Применяем управление в воздухе
        Vector2 currentVelocity = rb.velocity;
        currentVelocity.x += horizontalInput * airControlFactor;
        rb.velocity = new Vector2(Mathf.Clamp(currentVelocity.x, -moveSpeed, moveSpeed), currentVelocity.y);
    }

    void ApplyBrakes(float brakingFactor)
    {
        // Применяем торможение в воздухе
        float currentSpeed = rb.velocity.x;
        float newSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakingFactor * Time.deltaTime);
        rb.velocity = new Vector2(newSpeed, rb.velocity.y);
    }

    void UpdateAnimatorParameters(float horizontalInput)
    {
        // Обновляем параметры аниматора
        animator.SetFloat("HorizontalSpeed", horizontalInput);
        animator.SetBool("IsJumping", !isGrounded && rb.velocity.y > 0);
        animator.SetBool("IsMoving", Mathf.Abs(horizontalInput) > 0.1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
