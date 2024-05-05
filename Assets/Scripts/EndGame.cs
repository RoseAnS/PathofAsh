using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
public class EndGame : MonoBehaviour
{
    private static EndGame instance;
    [SerializeField] private GameObject[] cameras;


    private void Awake()
    {
        instance = this;
    }
    public void EndTheGame()
    {
        cameras[1].SetActive(true);
        cameras[0].SetActive(false);
    }

    public static EndGame GetInstance()
    {
        return instance;
    }
}
