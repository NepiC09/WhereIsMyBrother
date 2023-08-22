using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public void _OpenGallery() {
        PhoneGUI.Instance.OpenGalleryScreen();
    }
    public void _OpenMessanger()
    {
        PhoneGUI.Instance.OpenMessangerScreen();
    }
    public void _OpenInternet()
    {
        PhoneGUI.Instance.OpenInternetScreen();
    }
    public void _OpenCall()
    {
        PhoneGUI.Instance.OpenCallScreen();
    }
    public void _OpenSMS()
    {
        PhoneGUI.Instance.OpenSMSScreen();
    }
}
