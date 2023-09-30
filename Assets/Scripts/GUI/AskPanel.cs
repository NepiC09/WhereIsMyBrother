using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AskPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public UnityEvent<bool> onAskChoiced;

    public void onChoicePressed(bool isYes)
    {
        CloseAskPanel();
        onAskChoiced?.Invoke(isYes);
    }

    public void OpenAskPanel()
    {
        gameObject.LeanScale(Vector3.zero, 0f);
        gameObject.LeanScale(Vector3.one, 0.25f);
    }

    public void CloseAskPanel() {
        gameObject.LeanScale(Vector3.zero, 0.25f);
    }

    public void setText(string _text) {
        text.text = _text;
    }
}
