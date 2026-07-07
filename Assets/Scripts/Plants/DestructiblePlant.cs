using System;
using UnityEngine;

public class DestructiblePlant : MonoBehaviour
{
    public event EventHandler OnDestructibleTakeDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если объект коснулся меча
        if (collision.gameObject.GetComponent<Sword>())
        {
            // Вызываем событие (для визуальных эффектов и т.д.)
            OnDestructibleTakeDamage?.Invoke(this, EventArgs.Empty);

            // --- ДОБАВЛЕННЫЙ КОД ДЛЯ СПАВНА МОНЕТ ---
            // Пытаемся найти на этом же объекте скрипт LootSpawner
            if (TryGetComponent(out LootSpawner lootSpawner))
            {
                lootSpawner.SpawnLoot(); // Если нашли — выбрасываем монеты!
            }
            // ----------------------------------------

            // Уничтожаем объект растения/камня/пня
            Destroy(gameObject);

            // Обновляем навигацию для врагов
            if (NavMeshRebakeHelper.Instance != null)
            {
                NavMeshRebakeHelper.Instance.RequestRebake();
            }
        }
    }
}