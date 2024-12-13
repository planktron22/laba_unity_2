using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 2, если попали в MoveTo2
            if (gameObject.name == "MoveTo2")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 2
                }
            }

            // Деактивируем уровень 1, если попали в Remove1
            if (gameObject.name == "Remove1")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 1
                }
            }
        }
    }
}
