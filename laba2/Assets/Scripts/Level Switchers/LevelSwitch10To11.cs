using UnityEngine;

public class LevelSwitch10To11 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 11, если попали в MoveTo11
            if (gameObject.name == "MoveTo11")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 11
                }
            }

            // Деактивируем уровень 10, если попали в Remove10
            if (gameObject.name == "Remove10")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 10
                }
            }
        }
    }
}
