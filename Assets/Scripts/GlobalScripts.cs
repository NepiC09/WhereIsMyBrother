using System;
using System.Collections;
using UnityEngine;

public class GlobalScripts : MonoBehaviour 
{
    static public IEnumerator ActionAfterDialogue(Action action)
    {
        while (DialogueManager.Instance.isDialoguePlaying)
        {
            yield return null;
        }
        action();
    }
}
