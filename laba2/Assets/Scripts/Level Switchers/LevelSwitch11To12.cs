using UnityEngine;

public class LevelSwitch11To12 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 12, если попали в MoveTo12
            if (gameObject.name == "MoveTo12")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 12
                }
            }

            // Деактивируем уровень 11, если попали в Remove11
            if (gameObject.name == "Remove11")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 11
                }
            }
        }
    }
}
