using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Header("Префаб лута")]
    [SerializeField] private GameObject _lootPrefab;

    [Header("Настройки дропа")]
    [SerializeField] private int _minCoins = 1;
    [SerializeField] private int _maxCoins = 3;
    [SerializeField] private float _dropForce = 3f; // Сила разлета

    public void SpawnLoot()
    {
        if (_lootPrefab == null)
        {
            Debug.LogError($"На объекте {gameObject.name} не назначен префаб лута в LootSpawner!");
            return;
        }

        int count = Random.Range(_minCoins, _maxCoins + 1);

        for (int i = 0; i < count; i++)
        {
            // Спавним монету в точке врага
            GameObject coin = Instantiate(_lootPrefab, transform.position, Quaternion.identity);

            // Толкаем монету в случайную сторону
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * _dropForce, ForceMode2D.Impulse);
            }
        }
    }
}