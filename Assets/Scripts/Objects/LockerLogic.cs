using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LockerLogic : MonoBehaviour
{
    [Header("Indicator")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject lockerInside;
    [SerializeField] private GameObject firstButton;

    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        indicator.SetActive(true);
    }
    public void _HideIndicator()
    {
        indicator.SetActive(false);
    }

    public void _Interact()
    {
        _ShowLockerInside();
    }

    private void CancelPerformed(InputAction.CallbackContext obj)
    {
        _HideLockerInside();
    }

    public void _ShowLockerInside()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        PlayerController.Instance.setPlayable(false);

        lockerInside.transform.localScale = Vector3.zero;
        lockerInside.SetActive(true);
        lockerInside.LeanScale(Vector3.one, 0.25f);

        playerInput.currentActionMap.FindAction("Cancel").performed += CancelPerformed;
        //StartCoroutine(SelectFirstChoice());
    }

    public void _HideLockerInside()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        PlayerController.Instance.setPlayable(true);

        lockerInside.LeanScale(Vector3.zero, 0.25f).setOnComplete(() =>
        {
            lockerInside.SetActive(false);
        });
        
        playerInput.currentActionMap.FindAction("Cancel").performed -= CancelPerformed;
    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
}
