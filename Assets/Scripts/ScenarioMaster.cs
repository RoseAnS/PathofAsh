
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenarioMaster : MonoBehaviour

{


    [Header("Options")]
    [SerializeField] private TextAsset selectedScenario;
    [SerializeField] private string nextScene;

    [SerializeField] private GameObject[] choices;

    [SerializeField] private GameObject[] cameras;

    [Header("UI")]
    [SerializeField] private GameObject[] scenarioTextObject;

    private static ScenarioMaster instance; //does this thing part 1



    private TextMeshProUGUI[] choicesText; //keeps track of text in the choices
    private TextMeshProUGUI[] scenarioText;


    private int textBoxNumber;
    private int choiceSetNumber;

    private Story currentScenario; 

    private bool dialogueIsPlaying;

    private int camNumber;


    private const string CAMERA_TAG = "camera";

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
        int camIndex = 0;
        foreach (GameObject camera in cameras)
        {
            cameras[camIndex].SetActive(false);
            camIndex++;
        }


        //Gets all the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {

            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choices[index].SetActive(false);
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
        cameras[0].SetActive(true);
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


    }
    private void ContinueStory()
    {
        int index = textBoxNumber;
        if (currentScenario.canContinue) //add checks for tags here before displaying the next text
        {
            HandleTags(currentScenario.currentTags);
            scenarioTextObject[index].gameObject.SetActive(true);
            scenarioText[index].text = currentScenario.Continue();
            index++;
            textBoxNumber++;
            DisplayChoices();
        }
        else
        {
            NextScene();
        }
    }


    public void MakeChoice(int choiceIndex) //makes the choice based on the button pressed
    {
        Debug.Log("this is" + (choiceIndex));
        currentScenario.ChooseChoiceIndex(choiceIndex);
        currentScenario.Continue();
        ContinueStory();
        DisplayChoices();
    }
    public void DisableButton(int otherButtonNumber)
    {
        choices[otherButtonNumber].SetActive(false);
    }

    public void InkCheckTracker(bool boxCheck, bool flowerCheck, bool spikeCheck)
    {
        currentScenario.variablesState["boxPass"] = boxCheck;
        currentScenario.variablesState["flowerPass"] = flowerCheck;
        currentScenario.variablesState["spikePass"] = spikeCheck;
    }

    private void TransCamera()
    {
        camNumber++;
        cameras[camNumber].SetActive(true);

    }
    private void HandleTags(List<string> currentTags)
    {
        Debug.Log(currentTags);
        foreach (string tag in currentTags)
        {
            //parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogWarning("WARNING. Tag could not be parsed");
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            switch (tagKey)
            {
                case CAMERA_TAG:
                    Debug.Log("camera=" + tagValue);
                    TransCamera();
                    break;
                default:
                    Debug.LogWarning("Tag came in but cannot or is currently not being handled: " + tag);
                    break;
            }
        }
    }
    private void NextScene()
    {
        SceneManager.LoadScene(sceneName: nextScene);
    }
}
