using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public TextMeshProUGUI healthText;

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
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Игрок погиб!");
        Destroy(gameObject);
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString("F0");
        }
    }
}