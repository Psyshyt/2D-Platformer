using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxHealth = 100;

    private int currentHealth;
    private bool isGrounded;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Управление движением
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        // Прыжок по пробелу
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Проверка, находится ли персонаж на земле
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Ground"));
        if(isGrounded)
        {
            Debug.Log("Земля");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Если здоровье опускается до нуля, можно выполнить дополнительные действия, например, вызвать метод GameOver() или уничтожить объект.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Дополнительные действия при смерти персонажа
        Destroy(gameObject);
        // Или другие действия...
    }
}