using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Скорость движения персонажа
    public float jumpForce = 10f; // Сила прыжка
    public float timeBetweenJumps = 0.5f; // Время между прыжками

    private Rigidbody2D rb;
    private bool isGrounded; // Флаг, определяющий, находится ли персонаж на земле
    public Transform groundCheck; // Позиция для проверки, находится ли персонаж на земле
    private float lastJumpTime; // Время последнего прыжка

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Если объект GroundCheck не присвоен в инспекторе, выводим предупреждение
        if (groundCheck == null)
        {
            Debug.LogWarning("GroundCheck is not assigned. Make sure to assign it in the inspector.");
        }
    }

    void Update()
    {
        // Проверяем, находится ли персонаж на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Ground"));

        // Обработка ввода для движения по горизонтали
        float horizontalInput = Input.GetAxis("Horizontal");

        // Остановка персонажа, когда клавиша бега не нажата и он на земле
        if (isGrounded)
        {
            // Если ввода нет, устанавливаем горизонтальную скорость в ноль
            if (horizontalInput == 0)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            else
            {
                // Применяем скорость только при наличии ввода
                Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
                rb.velocity = movement;
            }
        }

        // Обработка прыжка
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && Time.time - lastJumpTime > timeBetweenJumps)
        {
            Jump();
        }

        // Выводим скорость в консоль
        Debug.Log("Speed: " + rb.velocity.x);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        lastJumpTime = Time.time; // Запоминаем время последнего прыжка
    }
}
