using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RayShooter : MonoBehaviour
{
    private Camera playerCamera;
    private bool isAutomatic = false;
    private int maxAmmo = 30;
    private int currentAmmo = 30;
    private float fireSpeed = 0.1f;
    private float reloadSpeed = 2.5f;
    private float nextFireTime;
    public TMP_Text ammoCounterText;

    private Transform fireOrigin;
    void Start()
    {
        playerCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        fireOrigin = new GameObject("FireOrigin").transform;
        fireOrigin.parent = playerCamera.transform;
        fireOrigin.localPosition = new Vector3(0, 0, 1);
        currentAmmo = maxAmmo;
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
        //Debug.Log($"Fire Origin Position: {fireOrigin.position}, Camera Forward: {_camera.transform.forward}");
        UpdateAmmoUI();
    }

    private void Fire()
    {
        // Установка позиции начала выстрела.
        fireOrigin.localPosition = new Vector3(0, 0, 1); // Измените согласно вашей модели

        // Получение точки, откуда будет произведен выстрел,
        // используя позицию и поворот персонажа.
        Vector3 firePoint = fireOrigin.position; // Используем fireOrigin для задания точки выстрела
        Vector3 fireDirection = fireOrigin.forward; // Используем вектор "вперед" для направления стрельбы

        Ray ray = new Ray(firePoint, fireDirection);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, ~LayerMask.GetMask("Player")))
        {
            GameObject hitObject = hit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

            if (target != null)
            {
                // Если цель была найдена, запускаем реакцию на попадание
                target.ReactToHit();
            }
            else
            {
                // Если цель не была найдена, создаем индикатор попадания в точку.
                StartCoroutine(SphereIndicatorCoroutine(hit.point, new Vector3(0.2f, 0.2f, 0.2f)));
                Debug.DrawLine(this.transform.position, hit.point, Color.green, 6);
            }
        }

        currentAmmo--; // Уменьшаем количество патронов
        nextFireTime = Time.time + fireSpeed; // Считаем время следующего выстрела
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
            ammoCounterText.text = $"AMMO: {currentAmmo}/{maxAmmo}";
            //Debug.Log($"Ammo UI Updated");
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

        float posX = playerCamera.pixelWidth / 2 - textSize.x / 2;
        float posY = playerCamera.pixelHeight / 2 - textSize.y / 2;

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