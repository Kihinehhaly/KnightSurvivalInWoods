using UnityEngine;
using TMPro; // Подключаем систему TextMeshPro для работы с текстом

public class CoinManager : MonoBehaviour
{
    // Статическая ссылка, чтобы другие скрипты (например, Coin) могли легко находить этот менеджер
    public static CoinManager instance;

    [Header("UI Настройки")]
    [SerializeField] private TextMeshProUGUI _coinText; // Ссылка на компонент текста на экране

    private int _coinCount = 0; // Переменная, где хранится текущее количество монет

    private void Awake()
    {
        // Настраиваем Одиночку (Singleton)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // При старте игры обновляем текст, чтобы там был "0"
        UpdateUI();
    }

    // Метод, который будут вызывать монетки при подборе
    public void AddCoin(int amount)
    {
        _coinCount += amount;
        Debug.Log($"Монета добавлена! Всего монет: {_coinCount}");
        UpdateUI(); // Обновляем текст на экране
    }

    // Метод для визуального обновления текста на экране
    private void UpdateUI()
    {
        if (_coinText != null)
        {
            _coinText.text = "Золото: " + _coinCount.ToString();
        }
        else
        {
            Debug.LogWarning("CoinManager: Не назначена ссылка на CoinText в Инспекторе!");
        }
    }
}