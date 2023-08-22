using UnityEngine;

public class HairpinsLogic : MonoBehaviour
{
    [SerializeField] private TextAsset getHairpins;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (GreenSegment.GetValue("isTalkedAfterSleepWithGrandma") && !GreenSegment.GetValue("isHairpinsGotten"))
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
        if (GreenSegment.GetValue("isTalkedAfterSleepWithGrandma") && !GreenSegment.GetValue("isHairpinsGotten"))
        {
            DialogueManager.Instance._StartDialogue(getHairpins);
            Inventory.SetValue("Hairpins", true);
            GreenSegment.SetValue("isHairpinsGotten", true);

            transform.parent.gameObject.SetActive(false);
            return;
        }
    }
}
