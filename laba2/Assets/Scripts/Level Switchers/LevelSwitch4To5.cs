using UnityEngine;

public class LevelSwitch4To5 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 5, если попали в MoveTo5
            if (gameObject.name == "MoveTo5")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 5
                }
            }

            // Деактивируем уровень 4, если попали в Remove1
            if (gameObject.name == "Remove4")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 4
                }
            }
        }
    }
}
