using UnityEngine;

public class KnifeLogic : MonoBehaviour
{
    [SerializeField] private TextAsset viewDialogue;
    [SerializeField] private TextAsset getKnife;
    [SerializeField] private GameObject parent;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (BlueSegment.GetValue("isKnifeViewed") == false)
        {
            indicator.SetActive(true);
            return;
        }

        if (BlueSegment.GetValue("isRequestedToLittleBoy") && !BlueSegment.GetValue("isKnifeGotten"))
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
        if(BlueSegment.GetValue("isKnifeViewed") == false)
        {
            DialogueManager.Instance._StartDialogue(viewDialogue);
            BlueSegment.SetValue("isKnifeViewed", true);

            StartCoroutine(GlobalScripts.ActionAfterDialogue(_ShowIndicator));
            return;
        }

        if(BlueSegment.GetValue("isRequestedToLittleBoy") && !BlueSegment.GetValue("isKnifeGotten"))
        {
            DialogueManager.Instance._StartDialogue(getKnife);
            Inventory.SetValue("Knife", true);
            BlueSegment.SetValue("isKnifeGotten", true);
            QuestManager.Instance.setCanTeleport(true);

            parent.SetActive(false);
            return;
        }
    }
}
