using UnityEngine;

public class BatterKitchen : MonoBehaviour
{
    [SerializeField] private TextAsset BatteryKitchen;
    [SerializeField] private GameObject parent;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (YellowSegment.GetValue("isTriedOffTV") && !YellowSegment.GetValue("isBatteryKitchenGotten"))
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
        if (YellowSegment.GetValue("isTriedOffTV") && !YellowSegment.GetValue("isBatteryKitchenGotten"))
        {
            DialogueManager.Instance._StartDialogue(BatteryKitchen);
            YellowSegment.SetValue("isBatteryKitchenGotten", true);
            Inventory.SetValue("BatteryKitchen", true);

            parent.SetActive(false);
            return;
        }
    }
}
