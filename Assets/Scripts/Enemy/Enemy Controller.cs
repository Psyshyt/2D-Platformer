using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f; // Скорость движения врага
    private Vector2 startingPosition; // Начальная позиция врага

    void Start()
    {
        // Сохраняем начальные координаты врага
        startingPosition = transform.position;
    }

    void Update()
    {
        // Используем Mathf.PingPong для движения влево и вправо
        float xPosition = Mathf.PingPong(Time.time * speed, 4) - 2; // 4 - это длина пути влево и вправо, -2 для центрирования

        // Устанавливаем новую позицию врага относительно начальной позиции
        transform.position = startingPosition + new Vector2(xPosition, 0f);
    }
}
