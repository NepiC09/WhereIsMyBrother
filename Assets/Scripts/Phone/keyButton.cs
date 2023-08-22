using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class keyButton : MonoBehaviour
{
    public UnityEvent<string> buttonClicked;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(InvokeClick);
        buttonClicked.AddListener(Keyboard.Instance.onKeyPressed);
        Keyboard.Instance.buttonArray.Add(button);
        GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.LowerCase;
    }

    public void InvokeClick()
    {
        buttonClicked?.Invoke(this.GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
