using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Настройки подбора")]
    [SerializeField] private float _pickupDelay = 0.5f; // Задержка перед подбором в секундах

    private float _currentTimer;

    private void Start()
    {
        // При появлении монеты запускаем таймер задержки
        _currentTimer = _pickupDelay;
    }

    private void Update()
    {
        // Пока таймер больше нуля, каждую секунду уменьшаем его
        if (_currentTimer > 0)
        {
            _currentTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если время задержки еще не прошло, игнорируем касание
        if (_currentTimer > 0) return;

        // Проверяем, есть ли на объекте, который коснулся монеты, скрипт Player
        if (collision.TryGetComponent(out Player player))
        {
            Debug.Log("Монетка успешно подобрана рыцарем (через GetComponent)!");

            // ТУТ в будущем будет прибавление к счетчику монет, например:
            // ScoreManager.instance.AddMoney(1);

            // Уничтожаем монету со сцены
            Destroy(gameObject);
        }
    }
}