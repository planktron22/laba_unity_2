using UnityEngine;

public class LevelSwitch9To10 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 10, если попали в MoveTo10
            if (gameObject.name == "MoveTo10")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 10
                }
            }

            // Деактивируем уровень 9, если попали в Remove9
            if (gameObject.name == "Remove9")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 9
                }
            }
        }
    }
}
