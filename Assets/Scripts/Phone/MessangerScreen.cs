using UnityEngine;

public class MessangerScreen : MonoBehaviour
{
    private GameObject currentMessage;

    public void OpenMessangerScreen()
    {
        gameObject.SetActive(true);
    }

    public void _LeftButtonPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;
        if (currentMessage == null)
        {
            MoveToMainScreen();
        }
        else
        {
            CloseMessageScreen();
        }
    }

    public void _RightButtonPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        MoveToMainScreen();
        CloseMessageScreen();
    }

    private void MoveToMainScreen()
    {
        PhoneGUI.Instance.OpenMainScreen();
        gameObject.SetActive(true);

        transform.LeanMoveLocalX(-500, 0.1f).setOnComplete(() => {
            gameObject.SetActive(false);
        });
    }

    public void _OpenMessageScreen(GameObject messageScreen)
    {
        currentMessage = messageScreen;
        messageScreen.SetActive(true);
        messageScreen.transform.LeanMoveLocalX(-500, 0f);
        messageScreen.transform.LeanMoveLocalX(0, 0.1f);
    }

    private void CloseMessageScreen()
    {
        if (currentMessage == null)
            return;

        currentMessage.transform.LeanMoveLocalX(0, 0f);
        currentMessage.transform.LeanMoveLocalX(-500, 0.1f).setOnComplete(() => {
            currentMessage.gameObject.SetActive(false);
            currentMessage = null;
        });
    }

    public void _UpdateDiary() {
        if (PhoneGUI.Instance.isMessangerChecked)
            return;
        PhoneGUI.Instance.isMessangerChecked = true;

        if (PhoneGUI.Instance.isSMSChecked)
        {
            Diary.SetValue("AndrewInCountry", true);
        }
    }

    public void _UpdateRedSegment()
    {
        RedSegment.SetValue("isMessagesViewed", true);
    }
}
