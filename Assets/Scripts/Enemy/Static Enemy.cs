using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public int damage = 1; // Урон, который статичный враг наносит игроку

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
