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

        // Проверяем, есть ли на коснувшемся объекте скрипт Player
        if (collision.TryGetComponent(out Player player))
        {
            Debug.Log("Монетка успешно подобрана рыцарем!");

            // --- ОБНОВЛЕННЫЙ КОД ТУТ ---
            // Стучимся к нашему менеджеру и просим прибавить 1 монетку
            if (CoinManager.instance != null)
            {
                CoinManager.instance.AddCoin(1);
            }
            // ---------------------------

            // Уничтожаем монету со сцены
            Destroy(gameObject);
        }
    }
}