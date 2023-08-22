using UnityEngine;

public class TVLogic : MonoBehaviour
{
    [SerializeField] private TextAsset tryOffTV;
    [SerializeField] private TextAsset OffTV;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (YellowSegment.GetValue("isRemoteTVGotten") && !YellowSegment.GetValue("isTriedOffTV"))
        {
            indicator.SetActive(true);
            return;
        }

        if (YellowSegment.GetValue("isBatteryAnnRoomGotten") && YellowSegment.GetValue("isBatteryKitchenGotten") && !YellowSegment.GetValue("isTVOffed"))
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
        if (YellowSegment.GetValue("isRemoteTVGotten") && !YellowSegment.GetValue("isTriedOffTV"))
        {
            DialogueManager.Instance._StartDialogue(tryOffTV);
            YellowSegment.SetValue("isTriedOffTV", true);
            StartCoroutine(GlobalScripts.ActionAfterDialogue(_ShowIndicator));
            return;
        }

        if(YellowSegment.GetValue("isBatteryAnnRoomGotten") && YellowSegment.GetValue("isBatteryKitchenGotten") && !YellowSegment.GetValue("isTVOffed"))
        {
            DialogueManager.Instance._StartDialogue(OffTV);
            YellowSegment.SetValue("isTVOffed", true);
            StartCoroutine(GlobalScripts.ActionAfterDialogue(_ShowIndicator));
            return;
        }
    }
}
