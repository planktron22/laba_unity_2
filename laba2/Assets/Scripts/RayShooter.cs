using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    private bool isAutomatic = false;
    private int maxAmmo = 30;
    private int currentAmmo = 30;
    private float fireSpeed = 0.1f;
    private float reloadSpeed = 2.5f;
    private float nextFireTime;
    [SerializeField] private TextMeshProUGUI ammoCounterText;

    private Transform fireOrigin;
    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        fireOrigin = new GameObject("FireOrigin").transform;
        fireOrigin.parent = _camera.transform;
        fireOrigin.localPosition = new Vector3(0, 0, 1);

        UpdateAmmoUI();
    }

    private void Update()
    {
        // ������������ ������ �������� � �������������� ������� T
        if (Input.GetKeyDown(KeyCode.T))
        {
            isAutomatic = !isAutomatic; // ������������ ������
            nextFireTime = Time.time + fireSpeed;
            Debug.Log("����� ��������: " + (isAutomatic ? "��������������" : "���������"));


        }

        // ��������, ����� �� ���
        if (Input.GetMouseButton(0) && currentAmmo > 0 && Time.time >= nextFireTime)
        {
            if (isAutomatic || Input.GetMouseButtonDown(0)) // ��������� ����� ��������
            {
                Fire();
                nextFireTime = Time.time + fireSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            Reload(); // �����������
        }

        // ���������� ���������� �����������
        Debug.Log($"Fire Origin Position: {fireOrigin.position}, Camera Forward: {_camera.transform.forward}");
    }

    private void Fire()
    {
        fireOrigin.localPosition = new Vector3(0, 0, 1);
        Vector3 firePoint = _camera.transform.position + _camera.transform.forward * 0.5f;
        Vector3 fireDirection = new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z).normalized;
        Debug.Log($"Camera Position: {_camera.transform.position}");
        Ray ray = new Ray(firePoint, fireDirection);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, ~LayerMask.GetMask("Player")))
        {
            GameObject hitObject = hit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

            if (target != null)
            {
                target.ReactToHit();
            }
            else
            {
                StartCoroutine(SphereIndicatorCoroutine(hit.point, new Vector3(0.2f, 0.2f, 0.2f)));
                Debug.DrawLine(this.transform.position, hit.point, Color.green, 6);
            }
        }

        currentAmmo--;
        nextFireTime = Time.time + fireSpeed;
        Debug.Log("-1");
        UpdateAmmoUI();
    }

    private void Reload()
    {
        nextFireTime = Time.time + reloadSpeed;
        currentAmmo = maxAmmo; // ���������� ���������� �������� �� ���������
        Debug.Log("�����������! ������ � ��� " + currentAmmo + " ��������.");
        UpdateAmmoUI();

    }
    private void UpdateAmmoUI()
    {
        if (ammoCounterText != null)
        {
            ammoCounterText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
            Debug.Log($"Ammo UI Updated");
        }
        else
        {
            Debug.Log("Ammo Counter Text is not assigned!");
        }
    }
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 12;
        style.normal.textColor = Color.white;

        string text = "*";
        Vector2 textSize = style.CalcSize(new GUIContent(text));

        float posX = _camera.pixelWidth / 2 - textSize.x / 2;
        float posY = _camera.pixelHeight / 2 - textSize.y / 2;

        GUI.Label(new Rect(posX, posY, textSize.x, textSize.y), text, style);
    }

    private IEnumerator SphereIndicatorCoroutine(Vector3 pos, Vector3 size)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        sphere.transform.localScale = size;

        yield return new WaitForSeconds(6);
        Destroy(sphere);
    }
}