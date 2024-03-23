using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RootDataController : MonoBehaviour
{
    [SerializeField]
    private RootData[] roots;

    public RootData Get(string key)
    {
        return roots.FirstOrDefault(t => t.Key == key);
    }

}

[Serializable]
public struct RootData
{
    public string Key;
    public string Title;
    public string Description;
}
