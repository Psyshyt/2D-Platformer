using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBuilder : MonoBehaviour
{
    public GameObject playerPrefab;      // Префаб игрока
    public GameObject platformPrefab;    // Префаб блока

    public int numberOfPlatforms = 10;   // Количество блоков

    void Start()
    {
        BuildSprites();
    }

    void BuildSprites()
    {
        // Создание игрока в углу слева
        GameObject player = Instantiate(playerPrefab, new Vector3(-4f, -2f, 0), Quaternion.identity);
        player.transform.parent = transform;
    }
}
