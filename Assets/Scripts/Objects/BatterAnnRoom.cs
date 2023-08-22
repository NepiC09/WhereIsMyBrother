using UnityEngine;

public class BatterAnnRoom : MonoBehaviour
{
    [SerializeField] private TextAsset BatteryAnnRoom;
    [SerializeField] private GameObject parent;

    public void _Interact()
    {
        if (YellowSegment.GetValue("isTriedOffTV") && !YellowSegment.GetValue("isBatteryAnnRoomGotten"))
        {
            DialogueManager.Instance._StartDialogue(BatteryAnnRoom);
            DialogueManager.Instance.ContinueStory();
            YellowSegment.SetValue("isBatteryAnnRoomGotten", true);
            Inventory.SetValue("BatteryAnnRoom", true);
            parent.SetActive(false);
            return;
        }
    }
}
