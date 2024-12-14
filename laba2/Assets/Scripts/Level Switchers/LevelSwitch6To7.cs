using UnityEngine;

public class LevelSwitch6To7 : MonoBehaviour
{
    public GameObject levelToActivate; // Укажите уровень, который нужно активировать
    public GameObject levelToDeactivate; // Укажите уровень, который нужно деактивировать

    private void OnTriggerEnter(Collider other)
    {
        // Здесь мы проверяем, что в триггер попал игрок
        if (other.CompareTag("Player"))
        {
            // Активируем уровень 7, если попали в MoveTo7
            if (gameObject.name == "MoveTo7")
            {
                if (levelToActivate != null)
                {
                    levelToActivate.SetActive(true); // Показываем Level 7
                }
            }

            // Деактивируем уровень 6, если попали в Remove6
            if (gameObject.name == "Remove6")
            {
                if (levelToDeactivate != null)
                {
                    levelToDeactivate.SetActive(false); // Прячем Level 6
                }
            }
        }
    }
}
