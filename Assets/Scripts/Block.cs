using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    public bool isBlocking;
    [SerializeField] SpriteRenderer blockSprite;

    public void Blocking(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            isBlocking = true;
            blockSprite.gameObject.SetActive(true);
        }
        if (value.canceled)
        {
            isBlocking = false;
            blockSprite.gameObject.SetActive(false);
        }
    }
}
