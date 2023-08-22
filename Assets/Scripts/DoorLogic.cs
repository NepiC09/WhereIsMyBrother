using System.Collections;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [Header("location to teleport")]
    [SerializeField] private string locationToTeleport;
    [SerializeField] private string location;

    [Header("Camera confiner")]
    [SerializeField] private Cinemachine.CinemachineConfiner cinemachineConfiner;
    [SerializeField] private CompositeCollider2D compositeCollider2D;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private float timeToFadeIn = 0.5f;
    private float timeToWait = 0.5f;
    private float timeToFadeOut = 0.5f;
    public static float summaryTIme { get; private set; }

    private void Awake()
    {
        summaryTIme = timeToFadeIn + timeToWait + timeToFadeOut;
    }

    private void Start()
    {
        indicator.SetActive(false);
    }

    public void _Teleport(Transform placeToTeleport)
    {
        if (BubbleDialogue.isBubblePlaying)
            return;

        if (!BlueSegment.GetValue("isPlayedFirstDialogue") && locationToTeleport != "Living Room")
        {
            return;
        }

        if (location == "Kitchen" && !QuestManager.Instance.canTeleport)
        {
            return;
        }

        //after knife
        if (location == "Hallway" && locationToTeleport == "Entrance" && !QuestManager.Instance.canTeleport)
        {
            return;
        }else if (location == "Hallway" && locationToTeleport != "Entrance" && !QuestManager.Instance.canTeleport)
        {
            QuestManager.Instance.setCanTeleport(true);
            Dad.Instance.gameObject.SetActive(true);
        }

        //Hide Dad
        if (locationToTeleport == "Kitchen" && BlueSegment.GetValue("isRequestedToLittleBoy") && !BlueSegment.GetValue("isKnifeGotten"))
        {
            Dad.Instance.gameObject.SetActive(false);
            QuestManager.Instance.setCanTeleport(false);
        }

        Fader.Instance.Teleport(placeToTeleport, timeToFadeIn, timeToWait, timeToFadeOut, compositeCollider2D);

        if (locationToTeleport == "Hallway" && BlueSegment.GetValue("isKnifeGotten") && !QuestManager.Instance.afterKnifeBubbleDialogue.isStoryPlayed)
        {
            QuestManager.Instance.setCanTeleport(false);
            StartCoroutine(StartKnifeBubble());
        }
        //cinemachineConfiner.m_BoundingShape2D = compositeCollider2D;
    }

    private IEnumerator StartKnifeBubble()
    {
        yield return new WaitUntil(Fader.Instance.isUnFaded);
        yield return new WaitForSeconds(1f);
        QuestManager.Instance.afterKnifeBubbleDialogue._StartDialogue();
    }

    public void _ShowIndicator()
    {
        if (!BlueSegment.GetValue("isPlayedFirstDialogue") && locationToTeleport != "Living Room")
        {
            _HideIndicator();
            return;
        }

        if (location == "Kitchen" && !QuestManager.Instance.canTeleport)
        {
            _HideIndicator();
            return;
        }
        indicator.SetActive(true);
    }
    public void _HideIndicator()
    {
        indicator.SetActive(false);
    }
}
