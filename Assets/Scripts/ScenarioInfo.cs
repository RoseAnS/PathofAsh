using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class ScenarioInfo : MonoBehaviour
{
    [Header("Scenario Intro")]
    public string scenarioTitle;
    public string scenarioDescription;
    [SerializeField] private string[] scenarioAttributes;


    [Header("Scenario Main")]
    public TextAsset scenarioInkStory; //holds the ink story for this scenario
    public int[] scenarioDiceChecks;


    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
