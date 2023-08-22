using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    public TextMeshProUGUI tmpUGUI { get; private set; }
    public bool isSelected {  get; private set; }

    private int normalFontSize = 60;
    private int selectedFontSize = 70;

    private void Start()
    {
        tmpUGUI = GetComponentInChildren<TextMeshProUGUI>();
        setSelected(false);
    }

    public void setSelected(bool selected)
    {
        isSelected = selected;
        if (selected) { 
            ShowSelected();
        }
        else
        {
            HideSelected();
        }
    }

    private void ShowSelected()
    {
        tmpUGUI.fontSize = selectedFontSize;
    }

    private void HideSelected()
    {
        tmpUGUI.fontSize = normalFontSize;
    }

    public void GetTmpUGUI()
    {
        if (tmpUGUI == null)
            tmpUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
}
