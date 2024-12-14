using UnityEngine;

public class LevelSwitch3To4 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 4, если попали в MoveTo4
            if (gameObject.name == "MoveTo4")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 4
                }
            }

            // Деактивируем уровень 3, если попали в Remove1
            if (gameObject.name == "Remove3")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 3
                }
            }
        }
    }
}
