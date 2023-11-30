using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Скорость движения врага
    public int damage = 1; // Урон, который враг наносит игроку
    public float moveDistance = 4f; // Расстояние, которое враг пройдет в каждую сторону

    private float moveDirection = 1f; // Направление движения: 1 - вправо, -1 - влево
    private float initialPositionX; // Начальная позиция врага по оси X

    private Animator animator; // Ссылка на компонент аниматора

    void Start()
    {
        // Получаем компонент аниматора
        animator = GetComponent<Animator>();

        // Сохраняем начальную позицию врага
        initialPositionX = transform.position.x;
    }

    void Update()
    {
        // Движение врага
        transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);

        // Проверка достижения максимальной дистанции в каждую сторону
        if (Mathf.Abs(transform.position.x - initialPositionX) >= moveDistance)
        {
            // Изменение направления движения при достижении максимальной дистанции
            moveDirection *= -1;

            // Обновление параметра анимации для отображения направления движения
            animator.SetFloat("Horizontal", moveDirection);
        }
    }

    // Обработка столкновений с другими объектами
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка, является ли объект игроком
        if (other.CompareTag("Player"))
        {
            // Получаем компонент HealthController у игрока
            HealthController playerHealth = other.GetComponent<HealthController>();

            // Проверка, что у нас есть компонент HealthController
            if (playerHealth != null)
            {
                // Вызываем метод TakeDamage у игрока, передавая урон в качестве параметра
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
