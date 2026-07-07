using UnityEngine;
using TMPro; // Обязательно для работы с TextMeshPro

public class CoinManager : MonoBehaviour
{
    // Синглтон, чтобы любой другой скрипт мог легко обратиться к менеджеру
    public static CoinManager Instance { get; private set; }

    [Header("UI Элементы")]
    [SerializeField] private TextMeshProUGUI _coinText; // Ссылка на текст интерфейса

    private int _coinCount = 0; // Переменная, где хранится количество монет

    private void Awake()
    {
        // Настройка синглтона
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI(); // При старте игры показываем "0"
    }

    // Метод, который будут вызывать монетки при подборе
    public void AddCoin(int amount)
    {
        _coinCount += amount;
        UpdateUI(); // Обновляем цифру на экране
    }

    // Метод для обновления текста на UI
    private void UpdateUI()
    {
        if (_coinText != null)
        {
            _coinText.text = _coinCount.ToString();
        }
    }
}