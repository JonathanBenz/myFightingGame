using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    public bool isBlocking;

    public void Blocking(InputAction.CallbackContext value)
    {
        if (value.performed)
            isBlocking = true;
        if (value.canceled)
            isBlocking = false;
    }
}
