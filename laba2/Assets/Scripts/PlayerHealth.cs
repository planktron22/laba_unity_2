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
            Debug.LogError("�� �������� healthText � PlayerHealth.");
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage. Damage:" + damage);
        currentHealth -= damage;
        Debug.Log("����� ������� ����: " + damage + ". ������� ��������: " + currentHealth);
        UpdateHealthText();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("����� �����!");
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