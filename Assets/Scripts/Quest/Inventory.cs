using System.Collections.Generic;
using UnityEngine;

public struct Inventory
{
    static private Dictionary<string, bool> conditions = new Dictionary<string, bool> 
    {
       {"ClosetKey", false},
       {"PoliceIcon", false},
       {"Knife", false},
       {"Phone", false},
       {"Helmet", false},
       {"SewingKit", false},
       {"RemoteTV", false},
       {"BatteryAnnRoom", false},
       {"BatteryKitchen", false},
       {"Hairpins", false},
    };

    public static bool GetValue(string key)
    {
        if (conditions.ContainsKey(key))
        {
            return conditions[key];
        }
        else
        {
            Debug.LogWarning("There's no key in conditions in Inventory: " + key);
            return false;
        }
    }

    public static void SetValue(string key, bool value)
    {
        if (conditions.ContainsKey(key))
        {
            conditions[key] = value;
            Debug.Log("Inventory: Key " + key + " was changed to: " + value);
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
