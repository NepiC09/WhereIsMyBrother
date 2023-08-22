using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private TextAsset getKey;

    public void _Interact()
    {
        if (BlueSegment.GetValue("isTalkedWithDadAfterCloset") && !BlueSegment.GetValue("isGotKeyToCloset"))
        {
            transform.parent.gameObject.transform.LeanScale(Vector3.zero, 0.1f).setOnComplete(() =>
            {
                transform.parent.gameObject.SetActive(false);
            });
            DialogueManager.Instance._StartDialogue(getKey);
            DialogueManager.Instance.ContinueStory();
            Inventory.SetValue("ClosetKey", true);
            BlueSegment.SetValue("isGotKeyToCloset", true);
            return;
        }
    }
}
