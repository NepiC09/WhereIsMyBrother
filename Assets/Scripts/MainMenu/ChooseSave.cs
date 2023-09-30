using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseSave : MonoBehaviour
{
    [SerializeField] private CanvasGroup faderCanvas;
    [SerializeField] private GameObject buttonGroup;
    [SerializeField] private SaveSystemManager saveSystemManager;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] bool isSavePanel;
    [SerializeField] AskPanel askPanel;

    private void Start()
    {
        if (isSavePanel)
        {
            foreach(Button button in buttonGroup.GetComponentsInChildren<Button>())
            {
                setButtonText(button);
                button.interactable = true;
            }
        }
        else
        {
            CheckSlots();
        }
        
    }

    private void CheckSlots()
    {
        foreach (Button button in buttonGroup.GetComponentsInChildren<Button>())
        {
            if (!saveSystemManager.isHaveSave(button.name + ".txt"))
            {
                button.interactable = false;
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Пустой слот";
            }
            else
            {
                setButtonText(button);
                button.interactable = true;
            }
        }
    }

    public void OpenChooseSave()
    {
        gameObject.LeanScale(Vector3.zero, 0f);
        gameObject.LeanScale(Vector3.one, 0.25f);
        playerInput.currentActionMap.FindAction("Cancel").performed += CancelPerformed;
        if (!isSavePanel)
        {
            CheckSlots();
        }
    }
    
    public void CloseChooseSave()
    {

        gameObject.LeanScale(Vector3.zero, 0.25f);
    }

    private void CancelPerformed(InputAction.CallbackContext context)
    {
        CloseChooseSave();
    }

    public void onLoadButtonPressed(string fileName)
    {
        GlobalScripts.currentSaveFileName = fileName + ".txt";
        
        faderCanvas.LeanAlpha(1f, 1f).setOnComplete(() =>
        {
            SceneManager.LoadScene(1);
            GlobalScripts.isStartedNewGame = false;
        });
    }

    //Костыль, как и всё остальное(
    public void onSaveButtonPressed(Button button)
    {
        if(pauseMenu != null && askPanel != null)
        {
            askPanel.setText("Сохранить в этом слоте?");
            askPanel.OpenAskPanel();
            askPanel.onAskChoiced.AddListener((bool isYes) => {
                if (isYes)
                {
                    GlobalScripts.currentSaveFileName = SaveSystemManager.Instance.fileName;
                    saveSystemManager.SaveGame(button.name + ".txt");

                    gameObject.LeanAlpha(1f, 0.05f).setOnComplete(() => { setButtonText(button); });
                    CloseChooseSave();
                }
                askPanel.onAskChoiced.RemoveAllListeners();
            });
        }
    }

    private void setButtonText(Button button)
    {
        if (saveSystemManager.isHaveSave(button.name + ".txt"))
        {
            DateTime date = saveSystemManager.returnLastWrite(button.name + ".txt");
            string text = date.Date.ToShortDateString() + "\n";
            if (date.Hour < 10) text += "0" + date.Hour.ToString();
            else text += date.Hour.ToString();

            text += ":";

            if (date.Minute < 10) text += "0" + date.Minute.ToString();
            else text += date.Minute.ToString();

            button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Пустой слот";
        }
    }
}
