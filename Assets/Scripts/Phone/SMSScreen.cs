using UnityEngine;

public class SMSScreen : MonoBehaviour
{
    private GameObject currentSMSScreen;

    public void OpenSMSScreen()
    {
        gameObject.SetActive(true);
        CloseSMSScreen();
        currentSMSScreen = null;
    }

    public void _LeftButtonPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;
        if (currentSMSScreen == null)
        {
            MoveToMainScreen();
        }
        else
        {
            CloseSMSScreen();
        }
    }

    public void _RightButtonPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        MoveToMainScreen();
        CloseSMSScreen();
    }

    private void MoveToMainScreen()
    {
        PhoneGUI.Instance.OpenMainScreen();
        gameObject.SetActive(true);

        transform.LeanMoveLocalY(-1000, 0.1f).setOnComplete(() => {
            gameObject.SetActive(false);
        });
    }

    public void _OpenSMSScreen(GameObject smsScreen)
    {
        currentSMSScreen = smsScreen;
        smsScreen.SetActive(true);
        smsScreen.transform.LeanMoveLocalX(-500, 0f);
        smsScreen.transform.LeanMoveLocalX(0, 0.1f);
    }

    private void CloseSMSScreen()
    {
        if (currentSMSScreen == null) 
            return;

        currentSMSScreen.transform.LeanMoveLocalX(0, 0f);
        currentSMSScreen.transform.LeanMoveLocalX(-500, 0.1f).setOnComplete(() => {
            currentSMSScreen.gameObject.SetActive(false);
            currentSMSScreen = null;
        });
    }

    public void _UpdateDiary()
    {
        if (PhoneGUI.Instance.isSMSChecked)
            return;
        PhoneGUI.Instance.isSMSChecked = true;

        if (PhoneGUI.Instance.isMessangerChecked)
        {
            Diary.SetValue("AndrewInCountry", true);
        }
    }
}
