using System.Collections.Generic;
using UnityEngine;

public struct YellowSegment
{
    static private Dictionary<string, bool> conditions = new Dictionary<string, bool>
    {
        {"isBasketBraked", false},
        {"isOrderViewed", false},
        {"isSewingKitGotten", false},
        {"isTriedToTalkAboutPresents", false},
        {"isRemoteTVGotten", false},
        {"isTriedOffTV", false},
        {"isBatteryAnnRoomGotten", false},
        {"isBatteryKitchenGotten", false},
        {"isTVOffed", false},
    };

    public static bool GetValue(string key)
    {
        if (conditions.ContainsKey(key))
        {
            return conditions[key];
        }
        else
        {
            Debug.LogWarning("There's no key in conditions in YellowSegment: " + key);
            return false;
        }
    }

    public static void SetValue(string key, bool value)
    {
        if (conditions.ContainsKey(key))
        {
            conditions[key] = value;
            Debug.Log("YellowSegment: Key " + key + " was changed to: " + value);
        }

        if (value == true)
        {
            DialogueManager.Instance.SetVariableState(key, new Ink.Runtime.IntValue(1));
        }
        else
        {

            DialogueManager.Instance.SetVariableState(key, new Ink.Runtime.IntValue(0));
        }
    }
}
