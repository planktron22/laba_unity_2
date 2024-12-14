using UnityEngine;

public class LevelSwitch7To8 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 8, если попали в MoveTo8
            if (gameObject.name == "MoveTo8")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 8
                }
            }

            // Деактивируем уровень 7, если попали в Remove7
            if (gameObject.name == "Remove7")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 7
                }
            }
        }
    }
}
