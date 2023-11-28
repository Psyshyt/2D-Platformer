using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Максимальное количество жизней
    private int currentHealth; // Текущее количество жизней
    private PlayerController playerController;

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем начальное количество жизней
        playerController = GetComponent<PlayerController>();
    }

    // Метод для уменьшения количества жизней
    public void TakeDamage()
    {
        currentHealth--;

        // Если жизни закончились, перезагружаем сцену (или примените другую логику поражения)
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Восстановление здоровья или другая логика при получении урона
            // Например, можно вызвать метод восстановления здоровья в PlayerController
            // playerController.RestoreHealth();
        }
    }
}
