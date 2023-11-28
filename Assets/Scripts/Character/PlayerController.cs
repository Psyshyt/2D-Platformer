using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Скорость движения персонажа
    public float jumpForce = 10f; // Сила прыжка
    private bool isGrounded; // Флаг, определяющий, находится ли персонаж на земле

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Проверяем, находится ли персонаж на земле
        isGrounded = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Ground"));

        // Обработка ввода для движения по горизонтали
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

        // Обработка прыжка
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Земля");
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}