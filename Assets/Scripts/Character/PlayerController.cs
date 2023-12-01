using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float airControlTimeLimit = 1f; // Ограничение времени управления в воздухе
    public float postJumpDelay = 1f; // Задержка после прыжка в секундах

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private float currentAirControlTime; // Текущее время управления в воздухе
    private float timeSinceLanded; // Время с момента приземления

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        // Получаем компонент Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            Move(horizontalInput);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (isGrounded && Mathf.Approximately(horizontalInput, 0f))
            {
                Stop();
            }

            currentAirControlTime = 0f;
            timeSinceLanded = 0f;
        }
        else
        {
            if (currentAirControlTime < airControlTimeLimit)
            {
                AirControl(horizontalInput);
                currentAirControlTime += Time.deltaTime;
            }
            else
            {
                // Если превышено время управления в воздухе, обнуляем задержку после приземления
                timeSinceLanded = 0f;
            }
        }

        // Проверяем задержку после приземления
        if (!isGrounded)
        {
            timeSinceLanded += Time.deltaTime;

            // Задержка истекла, персонаж может бежать
            if (timeSinceLanded >= postJumpDelay)
            {
                timeSinceLanded = 0f; // Сбрасываем время задержки
            }
        }

        // Обновляем параметры аниматора
        UpdateAnimatorParameters(horizontalInput);
    }

    void Move(float horizontalInput)
    {
        // Если прошло достаточно времени после приземления, разрешаем бег
        if (timeSinceLanded <= 0f)
        {
            float horizontalVelocity = Mathf.Clamp(horizontalInput * moveSpeed, -moveSpeed, moveSpeed);

            rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        }
    }

    void Jump()
    {
        // Если прошло достаточно времени после приземления, разрешаем прыжок
        if (timeSinceLanded <= 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Stop()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    void AirControl(float horizontalInput)
    {
        Vector2 currentVelocity = rb.velocity;
        currentVelocity.x += horizontalInput;
        rb.velocity = new Vector2(Mathf.Clamp(currentVelocity.x, -moveSpeed, moveSpeed), currentVelocity.y);
    }

    void UpdateAnimatorParameters(float horizontalInput)
    {
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
