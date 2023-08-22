using System.Collections;using UnityEngine;

public class BasketLogic : MonoBehaviour
{
    [SerializeField] private TextAsset brakeUpBasket;
    [SerializeField] private OrderLogic orderLogic;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (!YellowSegment.GetValue("isBasketBraked"))
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
        if (!YellowSegment.GetValue("isBasketBraked"))
        {
            DialogueManager.Instance._StartDialogue(brakeUpBasket);
            YellowSegment.SetValue("isBasketBraked", true);
            orderLogic.SetActiveTrue();
            orderLogic._ShowIndicator();
            StartCoroutine(GlobalScripts.ActionAfterDialogue(_ShowIndicator));
            return;
        }
    }
}
