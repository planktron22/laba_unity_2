using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public GameObject grenadePrefab;
    public float throwForce = 10f;
    public int maxGrenades = 3;
    private int currentGrenades;
    private Camera playerCamera;

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
        float radius = 1f;
        GameObject indicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        indicator.transform.position = explosionPosition;
        indicator.transform.localScale = Vector3.one * radius;

        Material material = new Material(Shader.Find("Standard")) { color = new Color(1, 0, 0, 0.5f) };
        indicator.GetComponent<Renderer>().material = material;

        yield return new WaitForSeconds(indicatorDuration);
        Destroy(indicator);
        Destroy(grenade);
    }

    void CreateImpactCircle(Vector3 position, float radius)
    {
        GameObject indicator = new GameObject("ImpactIndicator");
        LineRenderer lineRenderer = indicator.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 50;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Добавление материала для LineRenderer
        Material material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material = material;

        // Создание круга
        for (int i = 0; i < 50; i++)
        {
            float angle = i * Mathf.Deg2Rad * 360f / 50;
            float x = position.x + radius * Mathf.Cos(angle);
            float z = position.z + radius * Mathf.Sin(angle);
            lineRenderer.SetPosition(i, new Vector3(x, position.y, z));
        }
    }
}