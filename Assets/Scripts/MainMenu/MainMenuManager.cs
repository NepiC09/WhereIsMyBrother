using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup faderCanvas;
    [SerializeField] private SaveSystemManager saveSystemManager;
    [SerializeField] private ChooseSave chooseSave;
    [SerializeField] private OptionsMenu optionsMenu;

    private void Start()
    {
        faderCanvas.LeanAlpha(1f, 0f);
        faderCanvas.LeanAlpha(0f, 1f);
    }

    public void _OnNewGameButtonPressed()
    {
        faderCanvas.LeanAlpha(1f, 1f).setOnComplete(() =>
        {
            SceneManager.LoadScene(1);
            GlobalScripts.isStartedNewGame = true;
        });
    }
    public void _OnLoadGameButtonPressed()
    {
        chooseSave.OpenChooseSave();
    }
    public void _OnQuitGameButtonPressed()
    {
        Application.Quit();
    }
    public void _OnOptionMenuButtonPressed()
    {
        optionsMenu.OpenOptionsMenu();
    }
}
