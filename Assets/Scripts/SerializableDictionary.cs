using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary : Dictionary<Char, FutureCommand>, ISerializationCallbackReceiver
{

    [UnityEngine.SerializeField]
    private List<Char> keys = new List<Char>();

    [UnityEngine.SerializeField]
    private List<FutureCommand> values = new List<FutureCommand>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<Char, FutureCommand> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }

    public FutureCommand GetOrElse(Char letter, FutureCommand fc)
    {
        try
        {
            FutureCommand listing = this[letter];
            return listing;
        }
        catch (KeyNotFoundException)
        {
            FutureCommand listing = new FutureCommand("Do nothing", new List<Equation>());
            return listing;
        }
    }


}