using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink;
using System.Runtime.CompilerServices;

public class ScenarioController : MonoBehaviour
{
    [SerializeField] private GameObject scenarioStartButton;

    [Header("UI Panel")]
    [SerializeField] private GameObject scenarioMainPanel;
    [SerializeField] private GameObject scenarioCompletorPanel;
    [SerializeField] private GameObject scenarioCharacterPanel;
    [Space(10)] //puts space on the UI

    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI scenarioTitleText;
    [SerializeField] private TextMeshProUGUI scenarioBodyText;
    [SerializeField] private TextMeshProUGUI scenarioAttributesText;
    [SerializeField] private TextMeshProUGUI scenarioTraits1Text;
    [SerializeField] private TextMeshProUGUI scenarioTraits2Text;

    [Header("UI Text Object")]
    [SerializeField] private GameObject scenarioTitleObj;
    [SerializeField] private GameObject scenarioBodyObj;
    [SerializeField] private GameObject scenarioAttributesObj;
    [SerializeField] private GameObject scenarioTraits1Obj;
    [SerializeField] private GameObject scenarioTraits2Obj;
    [Header("UI Buttons")]

    [SerializeField] private GameObject charBoxButton;
    [SerializeField] private GameObject charSpikeButton;
    [SerializeField] private GameObject charFlowerpotButton;

    [Header("UI Display")]
    [SerializeField] private GameObject char1Image;
    [SerializeField] private GameObject char2Image;

    [Header("Character Holders")]
    [SerializeField] private string char1Holder;
    [SerializeField] private string char2Holder;

    // Character bool's
    private bool boxSelected;
    private bool spikeSelected;
    private bool flowerpotSelected;

    private void Start()
    {
        scenarioStartButton.SetActive(true);
        ScenarioReset();

    }
    private void ScenarioReset() //resets the scenario back to default 
    {
        scenarioMainPanel.SetActive(false);
        scenarioCompletorPanel.SetActive(false);
        scenarioCharacterPanel.SetActive(false);
        scenarioTitleObj.SetActive(false);
        scenarioBodyObj.SetActive(false);
        scenarioAttributesObj.SetActive(false);
        scenarioTraits1Obj.SetActive(false);
        scenarioTraits2Obj.SetActive(false);    

        //Empties the character holders
        char1Holder= string.Empty;
        char2Holder= string.Empty;

        // Resets character values

        boxSelected = false;
        spikeSelected = false;
        flowerpotSelected = false;
  
    }
    public void StartScenario() //start of the scenario trigger, happens before anything appears
    {
        Debug.Log("test");
        scenarioMainPanel.SetActive(true);
        scenarioStartButton.SetActive(false);
        RunScenario();
    }

    private void RunScenario()
    {
        scenarioTitleObj.SetActive(true);
        scenarioBodyObj.SetActive(true);
        scenarioAttributesObj.SetActive(true);
        scenarioCompletorPanel.SetActive(true);
        scenarioCharacterPanel.SetActive(true);


    }

    public void ChooseCharacters(string characterChoice)
    {

        if (char1Holder == characterChoice)
        {
            
        }

        else //runs the main 
        {
            if (char1Holder == string.Empty)
            {
                char1Holder = characterChoice;

                if (characterChoice == "box")
                {
                    boxSelected = true;
                    Debug.Log("check check");
                
                }
                else if (characterChoice == "spike")
                {
                    spikeSelected = true;
                }
                else if (characterChoice == "flowerpot")
                {
                    flowerpotSelected = true;
                }
            }
            else if (char2Holder == string.Empty)
            {
                char2Holder = characterChoice;

                if (characterChoice == "box")
                {
                    boxSelected = true;
                }
                else if (characterChoice == "spike")
                {
                    spikeSelected = true;
                }
                else if (characterChoice == "flowerpot")
                {
                    flowerpotSelected = true;
                }
            }
            else
            {
                Debug.Log("Character holders are full!");
            }
        }

    }


}
