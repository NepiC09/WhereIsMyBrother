using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    [SerializeField] private TextAsset afterGotSewingKit;
    [SerializeField] private TextAsset notViewedMessage;
    [SerializeField] private BoxGUI boxGUI;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

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
        //isBoxOpened
        if (!RedSegment.GetValue("isMessagesViewed"))
        {
            DialogueManager.Instance._StartDialogue(notViewedMessage);
            return;
        }
        if (RedSegment.GetValue("isMessagesViewed") && !YellowSegment.GetValue("isSewingKitGotten"))
        {
            boxGUI.OpenBoxGUI();
            return;
        }
        if (YellowSegment.GetValue("isSewingKitGotten"))
        {
            DialogueManager.Instance._StartDialogue(afterGotSewingKit);
            return;
        }
    }
}
