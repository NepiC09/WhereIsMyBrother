using TMPro;
using UnityEngine;

public class CallScreen : MonoBehaviour
{
    [SerializeField] private TextAsset wrongNumber;
    [SerializeField] private TextAsset correctNumber;

    [SerializeField] private TextMeshProUGUI numberSpace;

    private const string CORRECT_NUMBER = "889 271 041 ";

    public void OpenCallScreen()

    {
        gameObject.SetActive(true);
        numberSpace.text = "";
    }

    public void _LeftButtonPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        MoveToMainScreen();
    }

    public void _RightButtonPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        MoveToMainScreen();
    }
    private void MoveToMainScreen()
    {
        PhoneGUI.Instance.OpenMainScreen();
        gameObject.SetActive(true);

        transform.LeanMoveLocalY(-1000, 0.1f).setOnComplete(() => {
            gameObject.SetActive(false);
        });
    }


    public void _AddNumber(string number)
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        if (numberSpace.text.Replace(" ", string.Empty).Length == 9)
            return;

        numberSpace.text += number;

        if ( numberSpace.text.Replace(" ", string.Empty).Length % 3 == 0)
        {
            numberSpace.text += " ";
        }
    }

    public void _RemoveNumber()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;
        if (numberSpace.text.Length == 0)
            return;

        if (numberSpace.text[numberSpace.text.Length - 1] == ' ')
        {
            numberSpace.text = numberSpace.text.Remove(numberSpace.text.Length - 1, 1);
        }

        numberSpace.text = numberSpace.text.Remove(numberSpace.text.Length - 1, 1);
    }

    public void _Call()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        if (numberSpace.text == CORRECT_NUMBER && YellowSegment.GetValue("isTVOffed"))
        {
            DialogueManager.Instance._StartDialogue(correctNumber);
            DialogueManager.Instance.ContinueStory();
            if (!RedSegment.GetValue("isLawyerCalled"))
                RedSegment.SetValue("isLawyerCalled", true);
            if(!Diary.GetValue("GrandmaNumber"))
                Diary.SetValue("GrandmaNumber", true);
        } else
        {
            DialogueManager.Instance._StartDialogue(wrongNumber);
            DialogueManager.Instance.ContinueStory();
            numberSpace.text = "";
        }
    }
}
