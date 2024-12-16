using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller; // ���������� ���������
    public float speed = 5.0f; // �������� ��������
    public float gravity = -9.81f; // ����������
    public float jumpHeight = 1.0f; // ������ ������

    private Vector3 velocity; // ������ ��������
    private bool isGrounded; // ��������, ��������� �� �������� �� �����

    void Update()
    {
        // ���������, ��������� �� �������� �� �����
        isGrounded = controller.isGrounded;

        // ��������� ����� � ����������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ���������� ����������� ��������
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // ������� ���������
        controller.Move(move * speed * Time.deltaTime);

        // ��������� ������
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // ���������� ���� ��� ������
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // ��������� ����������
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ������������� ��������, ����� �������� ��������� �� ����
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // ��������� ����������
        }

        // ������� ��������� � ������ ����������
        controller.Move(velocity * Time.deltaTime);

      
    }
}