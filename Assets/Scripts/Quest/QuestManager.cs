using System.Collections;
using UnityEngine;

public class QuestManager : MonoBehaviour, IDataPersistance
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private TextAsset FirstDialogueWithDad;
    public BubbleDialogue afterKnifeBubbleDialogue;
    public BubbleDialogue angryDadBubbleDialogue;

    public bool canTeleport { get; private set; }

    private void Awake()
    {
        canTeleport = true;
        Instance = this;
    }
    public void LoadData(GameData gameData)
    {
        canTeleport = gameData.questManager_canTeleport;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.questManager_canTeleport = canTeleport;
    }

    public void setCanTeleport(bool canTp)
    {
        canTeleport = canTp;
    }

    public void _StartFirstDialgue()
    {
        if (BubbleDialogue.isBubblePlaying)
            return;
        //Should be played only once
        if (BlueSegment.GetValue("isPlayedFirstDialogue")) { return; }
        //Show dad and ba in the room

        //Start dialogue
        StartCoroutine(StartFirstDialogue());
    }

    private IEnumerator StartFirstDialogue()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(Fader.Instance.isUnFaded);

        if (!DialogueManager.Instance.isDialoguePlaying)
        {
            DialogueManager.Instance._StartDialogue(FirstDialogueWithDad);
            DialogueManager.Instance.ContinueStory();
        }

        Inventory.SetValue("Phone", true);
        BlueSegment.SetValue("isPlayedFirstDialogue", true);

        while (DialogueManager.Instance.isDialoguePlaying)
        {
            yield return null;
        }
        Diary.SetValue("FirstUpdate", true);
    }
}
