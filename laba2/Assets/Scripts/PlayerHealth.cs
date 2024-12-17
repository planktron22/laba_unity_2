using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public TextMeshProUGUI healthText;
    public float deathYThreshold = -5f; // Пороговое значение высоты для смерти

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        if (healthText == null)
        {
            Debug.LogError("Не назначен healthText в PlayerHealth.");
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage. Damage:" + damage);
        currentHealth -= damage;
        Debug.Log("Игрок получил урон: " + damage + ". Текущее здоровье: " + currentHealth);
        UpdateHealthText();
        if (currentHealth <= 0)
        {
            ReloadScene();
        }
    }


    void Update()
    {
        if (transform.position.y < deathYThreshold)
        {
            ReloadScene();
        }
    }
    private void ReloadScene()
    {
        Debug.Log("Игрок погиб!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString("F0");
        }
    }
}