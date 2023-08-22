using System.Collections;
using TMPro;
using UnityEngine;

public class MessageBoxes : MonoBehaviour
{
    public static MessageBoxes Instance { get; private set; }

    [Header("Up-Left box")]
    [SerializeField] private GameObject upLeftMessageBox;
    [SerializeField] private TextMeshProUGUI upLeftMessageTextBox;

    [Header("Down-Middle box")]
    [SerializeField] private GameObject downMiddleMessageBox;
    [SerializeField] private TextMeshProUGUI downMiddleMessageTextBox;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        upLeftMessageBox.SetActive(false);
        downMiddleMessageBox.SetActive(false);
    }

    public void OpenUpLeftMessage(string message) {
        upLeftMessageTextBox.text = message;
        upLeftMessageBox.GetComponent<RectTransform>().LeanMoveX(-500, 0f);
        upLeftMessageBox.SetActive(true);

        float timeToOpen = 0.25f;

        upLeftMessageBox.GetComponent<RectTransform>().LeanMoveX(0, timeToOpen).setOnComplete(() =>
        {
            float timeToWait = 3f;
            float timeToClose = 0.25f;

            Vector3 positionToClose = new Vector3(-500, upLeftMessageBox.GetComponent<RectTransform>().anchoredPosition.y);

            StartCoroutine(WaitAndCloseMessageBox(upLeftMessageBox, positionToClose, timeToWait, timeToClose));
        });
    }

    public void OpenDownMiddleMessage(string message) {
        downMiddleMessageTextBox.text = message;
        downMiddleMessageBox.GetComponent<RectTransform>().LeanMoveY(-500, 0f);
        downMiddleMessageBox.SetActive(true);

        float timeToOpen = 0.25f;

        downMiddleMessageBox.GetComponent<RectTransform>().LeanMoveY(0, timeToOpen).setOnComplete(() =>
        {
            float timeToWait = 5f;
            float timeToClose = 0.25f;

            Vector3 positionToClose = new Vector3(0, -500);

            StartCoroutine(WaitAndCloseMessageBox(downMiddleMessageBox, positionToClose, timeToWait, timeToClose));
        });
    }

    private IEnumerator WaitAndCloseMessageBox(GameObject messageBox, Vector3 postionToClose, float timeToWait,float timeToCLose) { 
        yield return new WaitForSeconds(timeToWait);
        messageBox.GetComponent<RectTransform>().LeanMove(postionToClose, timeToCLose).setOnComplete(() => { messageBox.SetActive(false); });
    }
}
