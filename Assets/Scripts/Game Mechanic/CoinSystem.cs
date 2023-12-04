using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{
    public Text coinCountText; // Ссылка на текстовый объект для отображения количества монет
    public int startingCoins = 0; // Начальное количество монет
    private int currentCoins; // Текущее количество монет

    void Start()
    {
        currentCoins = startingCoins;
        UpdateCoinCountText();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, столкнулся ли объект с монеткой
        if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject);
        }
    }

    // Метод для увеличения количества монет и удаления подобранной монеты
    void CollectCoin(GameObject coin)
    {
        currentCoins++;
        UpdateCoinCountText();

        // Удаляем монетку после подбора
        Destroy(coin);
    }

    // Метод для обновления текста с количеством монет
    void UpdateCoinCountText()
    {
        if (coinCountText != null)
        {
            coinCountText.text = currentCoins.ToString();
        }
    }
}
