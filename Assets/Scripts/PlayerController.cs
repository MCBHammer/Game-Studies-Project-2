using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
    FPSInput _input = null;
    FPSMotor _motor = null;

    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _sprintSpeed = .2f;
    [SerializeField] float _turnSpeed = 6f;
    [SerializeField] float _jumpStrength = 20f;

    float _currentMovementSpeed;

    private void Awake()
    {
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _currentMovementSpeed = _moveSpeed;
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
        _input.SprintDownInput += OnSprintDown;
        _input.SprintUpInput += OnSprintUp;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
        _input.SprintDownInput -= OnSprintDown;
        _input.SprintUpInput -= OnSprintUp;
    }

    void OnMove(Vector3 movement)
    {
        _motor.Move(movement * _currentMovementSpeed);
    }

    void OnRotate(Vector3 rotation)
    {
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);
    }

    void OnJump()
    {
        _motor.Jump(_jumpStrength);
    }

    void OnSprintDown()
    {
        _currentMovementSpeed = _sprintSpeed;
    }

    void OnSprintUp()
    {
        _currentMovementSpeed = _moveSpeed;
    }

}
