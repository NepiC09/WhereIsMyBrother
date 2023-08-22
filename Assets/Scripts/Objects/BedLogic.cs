using System.Collections;
using UnityEngine;

public class BedLogic : MonoBehaviour
{
    [SerializeField] private TextAsset sleepText;
    [SerializeField] private CompositeCollider2D annRomm;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (GreenSegment.GetValue("isTalkedAboutAndrewWithGrandma") && !GreenSegment.GetValue("isSlept"))
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
        if (GreenSegment.GetValue("isTalkedAboutAndrewWithGrandma") && !GreenSegment.GetValue("isSlept"))
        {
            DialogueManager.Instance._StartDialogue(sleepText);
            GreenSegment.SetValue("isSlept", true);
            StartCoroutine(GlobalScripts.ActionAfterDialogue(_ShowIndicator));
            StartCoroutine(Sleep());
            return;
        }
    }

    private IEnumerator Sleep()
    {
        while (DialogueManager.Instance.isDialoguePlaying)
        {
            yield return null;
        }
        Fader.Instance.FadeInOut(1f, 0.2f, 1f);
    }
}
