using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        XandY, X, Y
    }
    public RotationAxes _axes = RotationAxes.XandY;
    public float _rotationSpeedHor = 5.0f;
    public float _rotationSpeedVer = 5.0f;

    public float maxVert = 90.0f;
    public float minVert = -90.0f;

    private float _rotationX = 0;

    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    private void Update()
    {
        if (_axes == RotationAxes.XandY)
        {
            // Обработка вращения по вертикали
            _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;
            // Ограничиваем угол наклона
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            // Обработка вращения по горизонтали
            float deltaY = Input.GetAxis("Mouse X") * _rotationSpeedHor;
            float _rotationY = transform.localEulerAngles.y + deltaY;

            // Применяем вращение к камере
            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
        else if (_axes == RotationAxes.X)
        {
            // Вращение только по оси Y
            transform.Rotate(0, Input.GetAxis("Mouse X") * _rotationSpeedHor, 0);
        }
        else if (_axes == RotationAxes.Y)
        {
            // Обработка вращения по вертикали
            _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;
            // Ограничиваем угол наклона
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            // Оставляем угол Y неизменным
            float _rotationY = transform.localEulerAngles.y;

            // Применяем вращение к камере
            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
    }
}