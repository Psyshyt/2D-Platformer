using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName = "Gameplay 1";

    // Метод для загрузки первого уровня
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
