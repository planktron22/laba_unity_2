using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels; // Массив для хранения всех уровней
    private int currentLevelIndex = 0; // Индекс текущего уровня

    private void Start()
    {
        // Убедитесь, что только первый уровень активен
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Вывод отладочной информации в консоль
        Debug.Log("Triggered: " + other.gameObject.name);
        
        // Проверяем, столкнулись ли с объектом MoveTo
        if (other.CompareTag("MoveTo"))
        {
            MoveToNextLevel(other.gameObject);
        }
        // Проверяем, столкнулись ли с объектом Remove
        if (other.CompareTag("Remove"))
        {
            RemoveCurrentLevel(other.gameObject);
        }
    }

    void MoveToNextLevel(GameObject moveToObject)
    {
        // Определяем индекс следующего уровня
        int nextLevelIndex = currentLevelIndex + 1;

        // Проверяем, есть ли следующий уровень
        if (nextLevelIndex < levels.Length)
        {
            levels[nextLevelIndex].SetActive(true);
            currentLevelIndex = nextLevelIndex;
        }
        else
        {
            Debug.Log("Нет доступных уровней!");
        }
    }

    void RemoveCurrentLevel(GameObject removeObject)
    {
        // Удаляем текущий уровень
        if (currentLevelIndex > 0)
        {
            levels[currentLevelIndex].SetActive(false);
        }
    }
}
