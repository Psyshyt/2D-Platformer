using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    // Событие для смерти персонажа
    public event System.Action OnDeath;

    void Update()
    {
        // Проверка события смерти (если не null, то вызываем событие)
        OnDeath?.Invoke();
    }

    public void Death()
    {
        // Метод, который вызывается при смерти персонажа
        // Здесь можно добавить дополнительные действия, такие как анимация смерти, звук и т.д.

        // Обнуляем подписчиков события
        OnDeath = null;

        // Вызываем событие смерти для обработки в других скриптах
        OnDeath?.Invoke();
    }
}
