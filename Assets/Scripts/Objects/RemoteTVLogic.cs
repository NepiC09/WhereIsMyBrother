using UnityEngine;

public class RemoteTVLogic : MonoBehaviour
{
    [SerializeField] private TextAsset getRemoteTV;
    [SerializeField] private GameObject parent;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (YellowSegment.GetValue("isTriedToTalkAboutPresents") && !YellowSegment.GetValue("isRemoteTVGotten"))
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
        if (YellowSegment.GetValue("isSewingKitGotten") && !YellowSegment.GetValue("isRemoteTVGotten"))
        {
            DialogueManager.Instance._StartDialogue(getRemoteTV);
            YellowSegment.SetValue("isRemoteTVGotten", true);
            Inventory.SetValue("RemoteTV", true);
            parent.SetActive(false);
            return;
        }
    }
}
