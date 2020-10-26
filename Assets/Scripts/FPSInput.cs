using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSInput : MonoBehaviour
{
    [SerializeField] bool _invertVertical = false;

    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    public event Action JumpInput = delegate { };
    public event Action SprintDownInput = delegate { };
    public event Action SprintUpInput = delegate { };

    void Update()
    {
        DetectMoveInput();
        DetectRotateInput();
        DetectJumpInput();
        DetectSprintInput();
    }

    void DetectMoveInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        
        if(xInput != 0 || yInput != 0)
        {
            Vector3 _horizontalMovement = transform.right * xInput;
            Vector3 _forwardMovement = transform.forward * yInput;

            Vector3 movement = (_horizontalMovement + _forwardMovement).normalized;
            MoveInput?.Invoke(movement);
        }
    }

    void DetectRotateInput()
    {
        float xInput = Input.GetAxisRaw("Mouse X");
        float yInput = Input.GetAxisRaw("Mouse Y");

        if (xInput != 0 || yInput != 0)
        {
            if (_invertVertical)
            {
                yInput = -yInput;
            }

            Vector3 rotation = new Vector3(yInput, xInput, 0);
            RotateInput?.Invoke(rotation);
        }
    }

    void DetectJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput?.Invoke();
        }
    }

    void DetectSprintInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SprintDownInput?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SprintUpInput?.Invoke();
        }
    }

}
