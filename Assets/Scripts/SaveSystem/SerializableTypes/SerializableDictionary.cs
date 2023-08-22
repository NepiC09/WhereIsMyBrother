using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> keys = new List<Tkey>();
    [SerializeField] private List<Tvalue> values = new List<Tvalue>();
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach(KeyValuePair<Tkey, Tvalue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        this.Clear();

        if(keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys (" 
                + keys.Count + ") doesn't math with amont of values (" 
                + values.Count + ")");
        }

        for(int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }
}
