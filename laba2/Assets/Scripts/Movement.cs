using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float _speed = 6.0f;
    public float _gravity = -9.8f;
    private CharacterController _CharacterController;

    private void Start()
    {
        _CharacterController = GetComponent<CharacterController>();
        if (_CharacterController == null)
            Debug.Log("CharacterController is NULL");
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);
        movement *= Time.deltaTime;
        movement.y = _gravity;

        movement = transform.TransformDirection(movement);
        _CharacterController.Move(movement);
    }

}

