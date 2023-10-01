using System;
using System.Collections;

public static class GlobalScripts
{
    public static string language = "ru_RU";
    public static bool isStartedNewGame = false;
    public static string currentSaveFileName = "";

    static public IEnumerator ActionAfterDialogue(Action action)
    {
        while (DialogueManager.Instance.isDialoguePlaying)
        {
            yield return null;
        }
        action();
    }
}
