
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class ScenarioMaster : MonoBehaviour

{


    [Header("Options")]
    [SerializeField] private TextAsset selectedScenario;

    [SerializeField] private GameObject[] choices;

    [Header("UI")]
    [SerializeField] private GameObject[] scenarioTextObject;
    [SerializeField] private ScrollRect scrollView;

    private static ScenarioMaster instance; //does this thing part 1



    private TextMeshProUGUI[] choicesText; //keeps track of text in the choices
    private TextMeshProUGUI[] scenarioText;


    private int textBoxNumber;
    private int choiceSetNumber;

    private Story currentScenario; 

    private bool dialogueIsPlaying;


    private void Awake()
    {
        if (instance != null) //warns if there is ever more than one of a dialogue manager
        {
            Debug.LogWarning("Multiple of these, which is bad apparently.");
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Gets all the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {

            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;

        }
        scenarioText = new TextMeshProUGUI[scenarioTextObject.Length];
        StartDialogue(selectedScenario);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueIsPlaying) //we only want to update while there is dialogue, so return right away if dialogue isnt playing
        {
            return;
        }
        // handles getting to the next line of dialogue when submit is pressed. However we will need to mess with this because we are having 4 separate ways of displaying dialogue and may only want to press submit for some of them. 
        if (Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }
    }
    public static ScenarioMaster GetInstance() //does the thing part two
    {
        return instance;
    }

    public void StartDialogue(TextAsset inkJSON) //Temp. This will start the dialogue but I will need to add if statesments within using Ink tags to decide where the dialogue actually goes.
    {
        currentScenario = new Story(inkJSON.text); //sets the current story
        dialogueIsPlaying = true; //makes sure we know dialogue is playing
        int index = 0;
        foreach (GameObject text in scenarioTextObject)
        {
            scenarioTextObject[index].SetActive(false);
            scenarioText[index] = text.GetComponent<TextMeshProUGUI>();
            index++;
        }
        ContinueStory();

    }

    private void DisplayChoices()
    {

        List<Choice> currentChoices = currentScenario.currentChoices; //returns list of choices if there are any


        //looping through all of the choice objects and displaying them according to the current choices in the ink story.
 

        // enable and intialize the choices up to the amount of choices for this line of dialogue. 
        foreach (Choice choice in currentChoices)
        {
            choices[choiceSetNumber].gameObject.SetActive(true);
            choicesText[choiceSetNumber].text = choice.text;
            choiceSetNumber++;
        }

        // go through the remaining choices the UI supports and make sure they're hidden.
       // for (int i = index; i < choices.Length; i++)
       // {
       //     choices[i].gameObject.SetActive(false);
       // }

    }
    private void ContinueStory()
    {
        int index = textBoxNumber;
        if (currentScenario.canContinue) //add checks for tags here before displaying the next text
        {
            scenarioTextObject[index].gameObject.SetActive(true);
            scenarioText[index].text = currentScenario.Continue();
            index++;
            textBoxNumber++;
            DisplayChoices();
            StartCoroutine(ForceScrollDown()); //starts the autoscroller
        }
    }

    IEnumerator ForceScrollDown()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        scrollView.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    public void MakeChoice(int choiceIndex) //makes the choice based on the button pressed
    {
        Debug.Log("this is" + (choiceIndex));
        currentScenario.ChooseChoiceIndex(choiceIndex);
        currentScenario.Continue();
        ContinueStory();
        //makes sure the story continues and keeps going, deleting the options etc etc
        DisplayChoices();
    }

    public void InkCheckTracker(bool boxCheck, bool flowerCheck, bool spikeCheck)
    {
        currentScenario.variablesState["boxPass"] = boxCheck;
        currentScenario.variablesState["flowerPass"] = flowerCheck;
        currentScenario.variablesState["spikePass"] = spikeCheck;
    }

}
