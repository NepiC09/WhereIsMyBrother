using System.Collections;
using UnityEngine;

public class MailLogic : MonoBehaviour
{
    public static MailLogic Instance { get; private set; }

    [SerializeField] private TextAsset getMailText;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    [Header("Visual")]
    [SerializeField] private GameObject mailBoxOpen;
    [SerializeField] private GameObject mail;
    [SerializeField] LockGUI lockGUI;

    private void Awake()
    {
        Instance = this;   
    }

    private void Start()
    {
        indicator.SetActive(false);
        mailBoxOpen.SetActive(false);
        mail.SetActive(false);
    }
    public void _ShowIndicator()
    {
        if (GreenSegment.GetValue("isHairpinsGotten") && !GreenSegment.GetValue("isMailBoxHacked"))
        {
            indicator.SetActive(true);
            return;
        }
        if (GreenSegment.GetValue("isMailBoxHacked") && !GreenSegment.GetValue("isMailBoxOpened"))
        {
            indicator.SetActive(true);
            return;
        }
        if (GreenSegment.GetValue("isMailBoxOpened") && !GreenSegment.GetValue("isMailGotten"))
        {
            indicator.SetActive(true);
            return;
        }
        /*
        if (GreenSegment.GetValue("isHairpinsGotten") && !GreenSegment.GetValue("isMailGotten"))
        {
            indicator.SetActive(true);
            return;
        }
        */
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
        if(GreenSegment.GetValue("isHairpinsGotten") && !GreenSegment.GetValue("isMailBoxHacked"))
        {
            lockGUI.OpenLockGUI();
            return;
        }
        if(GreenSegment.GetValue("isMailBoxHacked") && !GreenSegment.GetValue("isMailBoxOpened"))
        {
            OpenMailBox();
            return;
        }
        if (GreenSegment.GetValue("isMailBoxOpened") && !GreenSegment.GetValue("isMailGotten"))
        {
            GetMail();
            return;
        }
        /*
        if (GreenSegment.GetValue("isHairpinsGotten") && !GreenSegment.GetValue("isMailGotten"))
        {
            DialogueManager.Instance._StartDialogue(mailText);

            StartCoroutine(EndGame());
            return;
        }
        */
    }

    private IEnumerator EndGame()
    {
        while (DialogueManager.Instance.isDialoguePlaying)
        {
            yield return null;
        }
        PlayerController.Instance.setPlayable(false);
        Fader.Instance.FadeIn(1f);
    }

    public void OpenMailBox()
    { 
        mailBoxOpen.SetActive(true);
        mail.SetActive(true);
        GreenSegment.SetValue("isMailBoxOpened", true);
    }
    private void GetMail()
    {
        mail.SetActive(false);
        DialogueManager.Instance._StartDialogue(getMailText);
        StartCoroutine(EndGame()); 
    }
}
