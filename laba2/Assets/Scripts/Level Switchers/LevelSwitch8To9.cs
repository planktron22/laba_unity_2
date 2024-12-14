using UnityEngine;

public class LevelSwitch8To9 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 9, если попали в MoveTo9
            if (gameObject.name == "MoveTo9")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 9
                }
            }

            // Деактивируем уровень 8, если попали в Remove8
            if (gameObject.name == "Remove8")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 8
                }
            }
        }
    }
}