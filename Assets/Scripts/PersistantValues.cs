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
        
        values.Add("Death Count", 0);
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
        }
    }
}
