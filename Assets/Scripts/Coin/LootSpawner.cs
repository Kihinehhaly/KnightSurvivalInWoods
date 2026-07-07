using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Header("Настройки лута")]
    [SerializeField] private GameObject _lootPrefab; // Префаб монеты
    [SerializeField] private int _minCoins = 3;       // Минимально монет
    [SerializeField] private int _maxCoins = 6;       // Максимально монет
    [SerializeField] private float _dropForce = 5f;   // Сила разлета

    public void SpawnLoot()
    {
        if (_lootPrefab == null)
        {
            Debug.LogError($"На объекте {gameObject.name} не назначен префаб лута в LootSpawner!");
            return;
        }

        // Случайное количество монет
        int count = Random.Range(_minCoins, _maxCoins + 1);

        for (int i = 0; i < count; i++)
        {
            // Создаем монету в точке гибели врага
            GameObject coin = Instantiate(_lootPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Генерируем случайное направление во все стороны (360 градусов)
                Vector2 randomDirection = Random.insideUnitCircle.normalized;

                // --- НАША ПОДСКАЗКА ДЛЯ ТЕСТА №2 ---
                // Эта строчка заставит Unity написать в консоль точные координаты направления
                Debug.Log($"[Тест 2] Монета {i + 1} летит в направлении: X = {randomDirection.x}, Y = {randomDirection.y}");
                // -----------------------------------

                // Толкаем монету
                rb.AddForce(randomDirection * _dropForce, ForceMode2D.Impulse);
            }
        }
    }
}