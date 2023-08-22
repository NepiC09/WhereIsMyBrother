using UnityEngine;

public class PhoneLogic : MonoBehaviour
{
    [SerializeField] private PhoneGUI phoneGUI;

    public void _Interact()
    {
        phoneGUI.OpenPhone();
    }
}
