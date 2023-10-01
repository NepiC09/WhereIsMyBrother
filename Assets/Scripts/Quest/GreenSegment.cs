using System.Collections.Generic;
using UnityEngine;

public struct GreenSegment
{
    static private Dictionary<string, bool> conditions = new Dictionary<string, bool>
    {
        {"isGreenStarted", false},
        {"isBubbleStarted", false },
        {"isTalkedAboutAndrewWithGrandma", false},
        {"isSlept", false},
        {"isTalkedAfterSleepWithGrandma", false},
        {"isHairpinsGotten", false },
        {"isMailBoxHacked", false },
        {"isMailBoxOpened", false },
        {"isMailGotten", false},
    };

    public static bool GetValue(string key)
    {
        if (conditions.ContainsKey(key))
        {
            return conditions[key];
        }
        else
        {
            //Debug.LogWarning("There's no key in conditions in GreenSegment: " + key);
            return false;
        }
    }

    public static void SetValue(string key, bool value)
    {
        if (conditions.ContainsKey(key))
        {
            conditions[key] = value;
            //Debug.Log("GreenSegment: Key " + key + " was changed to: " + value);
        }

        if (value == true)
        {
            DialogueManager.Instance.SetVariableState(key, new Ink.Runtime.IntValue(1));
            /*
            if (key == "isMailBoxOpened") { 
                MailLogic.Instance._Test_OpenMailBox();
            }
            if (key == "isMailGotten") {
                MailLogic.Instance._Test_GetMail();
            }
            */
            if(key == "isBubbleStarted")
            {
                QuestManager.Instance.angryDadBubbleDialogue._StartDialogue();
            }
        }
        else
        {

            DialogueManager.Instance.SetVariableState(key, new Ink.Runtime.IntValue(0));
        }
    }
}
