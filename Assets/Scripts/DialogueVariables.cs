using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class DialogueVariables{

    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }

    public void SetVariables(Dictionary<string, bool> var)
    {
        if (var.Count == 0)
        {
            foreach (string key in variables.Keys)
                var.Add(key, false);
        }

        foreach (string key in var.Keys)
        {
            if (var[key] == true)
            {
                variables.Remove(key);
                variables.Add(key, new Ink.Runtime.IntValue(1));
            }
            else
            {
                variables.Remove(key);
                variables.Add(key, new Ink.Runtime.IntValue(0));
            }

            BlueSegment.SetValue(key, var[key]);
            YellowSegment.SetValue(key, var[key]);
            RedSegment.SetValue(key, var[key]);
            GreenSegment.SetValue(key, var[key]);
            Inventory.SetValue(key, var[key]);
            Diary.SetValue(key, var[key]);
        }
    }
    

    public void LoadVariables()
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> pair in variables)
        {
            bool boolValue = false;
            
            if (pair.Value.ToString() == "1")
            {
                boolValue = true;
            }
            else if (pair.Value.ToString() == "0")
            {
                boolValue = false;
            }
            else
            {
                Debug.Log("That key with not boolean value: " + pair.Value.GetType().ToString());
            }

            BlueSegment.SetValue(pair.Key, boolValue);
            YellowSegment.SetValue(pair.Key, boolValue);
            RedSegment.SetValue(pair.Key, boolValue);
            GreenSegment.SetValue(pair.Key, boolValue);
            Inventory.SetValue(pair.Key, boolValue);
            Diary.SetValue(pair.Key, boolValue);
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }

        bool boolValue = false;
        if(value.ToString() == "1")
        {
            boolValue = true;
            Debug.Log("true " + value);
        }
        else if (value.ToString() == "0")
        {
            boolValue = false;
            Debug.Log("false " +  value);
        }
        else
        {
            Debug.Log(value.GetType().ToString());
            Debug.Log("Nothing");
        }

        BlueSegment.SetValue(name, boolValue);
        YellowSegment.SetValue(name, boolValue);
        RedSegment.SetValue(name, boolValue);
        GreenSegment.SetValue(name, boolValue);
        Inventory.SetValue(name, boolValue);
        Diary.SetValue(name, boolValue);
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
