using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BagLogic : MonoBehaviour
{ 
    [Header("Indicator")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject bagInside;
    [SerializeField] private GameObject firstButton;

    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (BlueSegment.GetValue("isTalkedWithDadAfterCloset") && !BlueSegment.GetValue("isGotKeyToCloset"))
        {
            indicator.SetActive(true);
            return;
        }
        _HideIndicator();
    }
    public void _HideIndicator()
    {
        indicator.SetActive(false);
    }

    public void _Interact()
    {
        if (BlueSegment.GetValue("isTalkedWithDadAfterCloset") && !BlueSegment.GetValue("isGotKeyToCloset"))
        {
            _ShowBagInside();
            return;
        }
    }

    private void CancelPerformed(InputAction.CallbackContext obj)
    {
        _HideBagInside();
    }

    public void _ShowBagInside()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        PlayerController.Instance.setPlayable(false);

        bagInside.transform.localScale = Vector3.zero;
        bagInside.SetActive(true);
        bagInside.LeanScale(Vector3.one, 0.25f);

        playerInput.currentActionMap.FindAction("Cancel").performed += CancelPerformed;
        //StartCoroutine(SelectFirstChoice());
    }

    public void _HideBagInside()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        PlayerController.Instance.setPlayable(true);

        bagInside.LeanScale(Vector3.zero, 0.25f).setOnComplete(() =>
        {
            bagInside.SetActive(false);
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
