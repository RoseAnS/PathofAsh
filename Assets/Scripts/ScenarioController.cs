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
    [SerializeField] private GameObject scenarioDicePanel;
    [Space(10)] //puts space on the UI

    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI scenarioTitleText;
    [SerializeField] private TextMeshProUGUI scenarioBodyText;
    [SerializeField] private TextMeshProUGUI scenarioAttributesText;
    [SerializeField] private TextMeshProUGUI scenarioTraits1Text;
    [SerializeField] private TextMeshProUGUI scenarioTraits2Text;
    [SerializeField] private TextMeshProUGUI dice1Text;
    [SerializeField] private TextMeshProUGUI dice2Text;

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

    //Dice Rolling
    private int diceOutcome1;
    private int diceOutcome2;
    private int diceTotalOutcome;


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
        scenarioDicePanel.SetActive(false);

        //Empties the character holders
        char1Holder = string.Empty;
        char2Holder= string.Empty;

        // Resets character values

        boxSelected = false;
        spikeSelected = false;
        flowerpotSelected = false;
        // Resets all the dice values
        diceOutcome1 = 0;
        diceOutcome2 = 0;
        diceTotalOutcome = 0;
  
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
        //checks if either of the holders contain the current character choice
        if (char1Holder == characterChoice)
        {
            if (boxSelected == true)
            {
                char1Holder = string.Empty;
                boxSelected = false;
            }
            else if (spikeSelected == true)
            {
                char1Holder = string.Empty;
                spikeSelected = false;
            }
            else if (flowerpotSelected == true)
            {
                char1Holder = string.Empty;
                flowerpotSelected = false;
            }
            //tells the traits to empty out 
            FillTraits(1, false, char1Holder);
        }
        else if (char2Holder == characterChoice)
        {
            if (boxSelected == true)
            {
                char2Holder = string.Empty;
                boxSelected = false;
            }
            else if (spikeSelected == true)
            {
                char2Holder = string.Empty;
                spikeSelected = false;
            }
            else if (flowerpotSelected == true)
            {
                char2Holder = string.Empty;
                flowerpotSelected = false;
            }
            //tells the traits to empty out 
            FillTraits(2, false, char2Holder);
        }
        else //runs the main thing if none of the holders contain the clicked character
        {
            if (char1Holder == string.Empty)
            {
                char1Holder = characterChoice;

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
                FillTraits(1,true,char1Holder);
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
                FillTraits(2, true, char2Holder);
            }
            else
            {
                Debug.Log("Character holders are full!");
            }
        }

    }
    private void FillTraits(int charHolder, bool removeOrAdd, string characterSelection)
    {
        //checks if the information is coming from character holder 1 or 2
        if (charHolder == 1)
        {
            //checks if it needs to remove or add the traits
            if (removeOrAdd == true)
            {
                //sets the text depending on the character
                if (characterSelection == "box")
                {
                    scenarioTraits1Text.text = "this contains box's info";
                }
                else if (characterSelection == "spike")
                {
                    scenarioTraits1Text.text = "this contains spikes's info";
                }
                else if (characterSelection == "flowerpot")
                {
                    scenarioTraits1Text.text = "this contains flowerpot's info";
                }
                else
                {
                    Debug.Log("something has gone wron and no character is currently selected");
                }
                scenarioTraits1Obj.SetActive(true);
            }
            //deactives the text and makes it empty
            else if (removeOrAdd == false)
            {
                scenarioTraits1Text.text = string.Empty;
                scenarioTraits1Obj.SetActive(false);
            }

        }
        else if (charHolder == 2)
        {
            scenarioTraits2Obj.SetActive(true);
            //checks if it needs to remove or add the traits
            if (removeOrAdd == true)
            {
                //sets the text depending on the character
                if (characterSelection == "box")
                {
                    scenarioTraits2Text.text = "this contains box's info";
                }
                else if (characterSelection == "spike")
                {
                    scenarioTraits2Text.text = "this contains spikes's info";
                }
                else if (characterSelection == "flowerpot")
                {
                    scenarioTraits2Text.text = "this contains flowerpot's info";
                }
                else
                {
                    Debug.Log("something has gone wron and no character is currently selected");
                }
                scenarioTraits2Obj.SetActive(true);
            }
            //deactives the text and makes it empty
            else if (removeOrAdd == false)
            {
                scenarioTraits2Text.text = string.Empty;
                scenarioTraits2Obj.SetActive(false);
            }
        }
    }
    private void RollTheDice() //Rolls the dice and totals them up
    {
        diceOutcome1 = Random.Range(1, 7);
        diceOutcome2 = Random.Range(1, 7);
        Debug.Log(diceOutcome1 + " " + diceOutcome2);

        diceTotalOutcome = diceOutcome1 + diceOutcome2;
        Debug.Log(diceTotalOutcome);
        dice1Text.text = diceOutcome1.ToString();
        dice2Text.text = diceOutcome2.ToString();
        scenarioDicePanel.SetActive(true);
    }
    public void ScenarioGo() //triggers when the Go button is pressed for the scenario
    {
        RollTheDice();
    }
}
