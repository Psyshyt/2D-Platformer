using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : MonoBehaviour
{
    public string nextLevelName; // Название следующего уровня

    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка, является ли объект игроком
        if (other.CompareTag("Player"))
        {
            // Загрузка следующего уровня
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        // Проверка, есть ли следующий уровень
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            // Загрузка следующего уровня по его названию
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            // Если название следующего уровня не указано, выводим предупреждение
            Debug.LogWarning("Next level name is not specified!");
        }
    }
}
