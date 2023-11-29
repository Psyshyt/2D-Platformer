using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 3; // Максимальное количество жизней
    private int currentHealth; // Текущее количество жизней

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем начальное количество жизней
    }

    // Метод для уменьшения количества жизней
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Если жизни закончились, перезагружаем сцену (или примените другую логику поражения)
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
