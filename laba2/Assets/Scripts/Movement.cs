using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller; // Контроллер персонажа
    public float speed = 5.0f; // Скорость движения
    public float gravity = -9.81f; // Гравитация
    public float jumpHeight = 1.0f; // Высота прыжка

    private Vector3 velocity; // Вектор скорости
    private bool isGrounded; // Проверка, находится ли персонаж на земле

    void Update()
    {
        // Проверяем, находится ли персонаж на земле
        isGrounded = controller.isGrounded;

        // Обработка ввода с клавиатуры
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Определяем направление движения
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Двигаем персонажа
        controller.Move(move * speed * Time.deltaTime);

        // Обработка прыжка
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Добавление силы для прыжка
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Применяем гравитацию
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Устанавливаем значение, чтобы персонаж оставался на полу
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // Применяем гравитацию
        }

        // Двигаем персонажа с учетом гравитации
        controller.Move(velocity * Time.deltaTime);

      
    }
}