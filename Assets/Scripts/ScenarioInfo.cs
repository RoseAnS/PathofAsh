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
    [SerializeField] private string scenarioTitle;
    [SerializeField] private string scenarioDescription;
    [SerializeField] private string[] scenarioAttributes;


    [Header("Scenario Main")]
    [SerializeField] private TextAsset scenarioInkStory; //holds the ink story for this scenario
    [SerializeField] private int[] scenarioDiceChecks;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
