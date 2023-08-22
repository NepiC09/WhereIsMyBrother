using System.Collections.Generic;
using UnityEngine;

public struct BlueSegment
{
    static private Dictionary<string, bool> conditions = new Dictionary<string, bool>
    {
        {"isPlayedFirstDialogue", false},
        {"isKnifeViewed", false},
        {"isFirstTalkedWithLittleBoy", false},
        {"isClosetViewed", false},
        {"isTalkedWithDadAfterCloset", false},
        {"isGotKeyToCloset", false},
        {"isOpenedCloset", false},
        {"isGotIcon", false},
        {"isGivenIconAndGottenHelmet", false},
        {"isRequestedToLittleBoy", false},
        {"isKnifeGotten", false}
    };

    public static bool GetValue(string key)
    {
        if (conditions.ContainsKey(key))
        {
            return conditions[key];
        }
        else
        {
            Debug.LogWarning("There's no key in conditions in BlueSegment: " + key);
            return false;
        }
    }

    public static void SetValue(string key, bool value)
    {
        if (conditions.ContainsKey(key))
        {
            conditions[key] = value;
            Debug.Log("BlueSegment: Key " + key + " was changed to: " + value);
        }

        if (value == true)
        {
            DialogueManager.Instance.SetVariableState(key, new Ink.Runtime.IntValue(1));

            //Получаем шлем, снимаем шлем с мальчика
            if(key == "isGivenIconAndGottenHelmet")
            {
                LittleBoy.Instance.LooseHelmet();
            }
        }
        else
        {
            DialogueManager.Instance.SetVariableState(key, new Ink.Runtime.IntValue(0));
        }
    }
}
