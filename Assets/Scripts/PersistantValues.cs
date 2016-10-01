using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistantValues : MonoBehaviour {

    public static PersistantValues Instance
    {
        get;
        private set;
    }

    private Dictionary<string,int> values;
    private Dictionary<string,int> initValues;

    // Use this for initialization
    void Awake () {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        values = new Dictionary<string, int>();
        initValues = new Dictionary<string, int>();


        values.Add("Death Count", 0);
        initValues.Add("Death Count", 0);
    }

    public int GetValue(string key)
    {
        return values[key];
    }

    public void SetValue(string key, int value)
    {
        if (values.ContainsKey(key))
        {
            values[key] = value;
        }
        else
        {
            values.Add(key, value);
            initValues.Add(key, value);
        }
    }

    public void ResetValues()
    {
        foreach (string key in values.Keys)
        {
            values[key] = initValues[key];
        }
    }
}
