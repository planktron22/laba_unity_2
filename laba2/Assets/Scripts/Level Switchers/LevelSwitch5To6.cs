using UnityEngine;

public class LevelSwitch5To6 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 6, если попали в MoveTo6
            if (gameObject.name == "MoveTo6")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 6
                }
            }

            // Деактивируем уровень 5, если попали в Remove5
            if (gameObject.name == "Remove5")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 5
                }
            }
        }
    }
}
