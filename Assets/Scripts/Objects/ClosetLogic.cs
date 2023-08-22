using UnityEngine;

public class ClosetLogic : MonoBehaviour
{
    [SerializeField] private TextAsset viewDialogue;
    [SerializeField] private TextAsset openCloset;
    [SerializeField] private TextAsset getIcon;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (BlueSegment.GetValue("isClosetViewed") == false && BlueSegment.GetValue("isFirstTalkedWithLittleBoy"))
        {
            indicator.SetActive(true);
            return;
        }

        if (BlueSegment.GetValue("isGotKeyToCloset") && !BlueSegment.GetValue("isOpenedCloset"))
        {
            indicator.SetActive(true);
            return;
        }
        if (BlueSegment.GetValue("isOpenedCloset") && !BlueSegment.GetValue("isGotIcon"))
        {
            indicator.SetActive(true);
            return;
        }
        //remove if can't interact
        transform.parent.GetComponentInChildren<InteractiveObject>().RemoveFromInteractiveArray();
        _HideIndicator();
    }
    public void _HideIndicator()
    {
        indicator.SetActive(false);
    }

    public void _Interact()
    {
        if (BlueSegment.GetValue("isClosetViewed") == false && BlueSegment.GetValue("isFirstTalkedWithLittleBoy"))
        {
            DialogueManager.Instance._StartDialogue(viewDialogue);
            BlueSegment.SetValue("isClosetViewed", true);
            StartCoroutine(GlobalScripts.ActionAfterDialogue(_ShowIndicator));
            return;
        }

        if (BlueSegment.GetValue("isGotKeyToCloset") && !BlueSegment.GetValue("isOpenedCloset"))
        {
            DialogueManager.Instance._StartDialogue(openCloset);
            BlueSegment.SetValue("isOpenedCloset", true);
            StartCoroutine(GlobalScripts.ActionAfterDialogue(_ShowIndicator));
            return;
        }
        if (BlueSegment.GetValue("isOpenedCloset") && !BlueSegment.GetValue("isGotIcon"))
        {
            DialogueManager.Instance._StartDialogue(getIcon);
            Inventory.SetValue("PoliceIcon", true);
            BlueSegment.SetValue("isGotIcon", true);
            StartCoroutine(GlobalScripts.ActionAfterDialogue(_ShowIndicator));
            return;
        }
    }
}
