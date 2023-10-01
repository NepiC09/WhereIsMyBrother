using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public void OpenOptionsMenu()
    {
        gameObject.LeanScale(Vector3.zero, 0f);
        gameObject.LeanScale(Vector3.one, 0.25f);
        playerInput.currentActionMap.FindAction("Cancel").performed += CancelPerformed;
    }

    public void CloseOptionsMenu()
    {
        gameObject?.LeanScale(Vector3.zero, 0.25f);
    }
    private void CancelPerformed(InputAction.CallbackContext context)
    {
        CloseOptionsMenu();
    }
}
