using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public GameObject grenadePrefab;
    public float throwForce = 10f;
    public int maxGrenades = 3;
    private int currentGrenades;
    private Camera playerCamera;
    public TMP_Text nadeCounterText;

    void Start()
    {
        currentGrenades = maxGrenades;
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && currentGrenades > 0)
        {
            ThrowGrenade();
            currentGrenades--;
            UpdateNadeUI();
            Debug.Log("Grenade!");

        }
    }

    void ThrowGrenade()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
            return;
        }

        // Создание гранаты
        GameObject grenade = Instantiate(grenadePrefab, playerCamera.transform.position, Quaternion.identity);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = grenade.AddComponent<Rigidbody>();
        }

        rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);

        // Начать корутину для отображения индикатора взрыва
        StartCoroutine(DrawImpactIndicator(grenade));
    }

    private void UpdateNadeUI()
    {      
        nadeCounterText.text = $"GRENADES: {currentGrenades}/{maxGrenades}";     
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что объект с которым мы соприкасаемся имеет тег "LevelTrigger"
        if (other.CompareTag("LevelTrigger"))
        {
            currentGrenades = maxGrenades; // Восстанавливаем количество гранат
            UpdateNadeUI(); // Обновляем UI
            Debug.Log("Grenades replenished!");
        }
    }


    private IEnumerator DrawImpactIndicator(GameObject grenade)
    {
        // Ждем немного, чтобы позволить гранате переместиться
        yield return new WaitForSeconds(1.6f);

        // Получаем позицию гранаты
        Vector3 explosionPosition = grenade.transform.position;

        // Добавляем небольшое смещение вперёд по направлению полета
        Vector3 offset = grenade.GetComponent<Rigidbody>().velocity.normalized;
        explosionPosition += offset * 2.0f; // Сместим на 2 метра вперед

        float indicatorDuration = 1f;
        float radius = 8f;
        GameObject indicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        indicator.transform.position = explosionPosition;
        indicator.transform.localScale = Vector3.one * radius;

        Material material = new Material(Shader.Find("Standard")) { color = new Color(1, 0, 0, 0.5f) };
        indicator.GetComponent<Renderer>().material = material;

        yield return new WaitForSeconds(indicatorDuration);
        Destroy(indicator);
        Destroy(grenade);
    }

    public void CreateImpactCircle(Vector3 position, float radius)
    {
        // Создание круга
        for (int i = 0; i < 50; i++)
        {
            float angle = i * Mathf.Deg2Rad * 360f / 50;
            float x = position.x + radius * Mathf.Cos(angle);
            float z = position.z + radius * Mathf.Sin(angle);

            // Создаем точки для Visual representation (если нужно)
            Vector3 point = new Vector3(x, position.y, z);
            // Здесь можно использовать Instantiate для создания объектов визуализации, если требуется
        }

        // Добавление Collider для обнаружения столкновений
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        foreach (var hitCollider in hitColliders)
        {
            ReactiveTarget target = hitCollider.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                target.ReactToHit();
            }
        }
    }
}