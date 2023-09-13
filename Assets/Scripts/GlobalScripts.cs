using System;
using System.Collections;
using UnityEngine;

public static class GlobalScripts
{
    public const string RU_LOCALIZATION = "ru_RU";
    public static string language;
    public static bool isStartedNewGame = false;

    static public IEnumerator ActionAfterDialogue(Action action)
    {
        while (DialogueManager.Instance.isDialoguePlaying)
        {
            yield return null;
        }
        action();
    }
}
