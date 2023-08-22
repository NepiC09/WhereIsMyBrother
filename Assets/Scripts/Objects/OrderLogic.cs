using UnityEngine;

public class OrderLogic : MonoBehaviour
{
    [SerializeField] private TextAsset viewOrder;
    [SerializeField] private GameObject parent;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
        parent.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (YellowSegment.GetValue("isBasketBraked") && !YellowSegment.GetValue("isOrderViewed"))
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
        if (YellowSegment.GetValue("isBasketBraked") && !YellowSegment.GetValue("isOrderViewed"))
        {
            DialogueManager.Instance._StartDialogue(viewOrder);
            YellowSegment.SetValue("isOrderViewed", true);
            Diary.SetValue("SomebodyOrderStuffs", true);

            parent.SetActive(false);
            return;
        }
    }

    public void SetActiveTrue()
    {
        parent.SetActive(true);
    }
}
