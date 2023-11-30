using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestartTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");
            // Перезагрузка текущего уровня
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
