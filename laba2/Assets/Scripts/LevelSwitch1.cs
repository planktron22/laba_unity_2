using UnityEngine;

public class LevelSwitch1 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 3, если попали в MoveTo3
            if (gameObject.name == "MoveTo3")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 3
                }
            }

            // Деактивируем уровень 2, если попали в Remove1
            if (gameObject.name == "Remove2")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 2
                }
            }
        }
    }
}
