using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{

    private static DialogueController instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI boxText;
    [SerializeField] private TextMeshProUGUI flowerText;
    [SerializeField] private TextMeshProUGUI spikeText;
    [SerializeField] private GameObject boxHolder;
    [SerializeField] private GameObject flowerHolder;
    [SerializeField] private GameObject spikeHolder;


    [Header("Convo Info")]
    [SerializeField] private string nextSpeaker;
    [SerializeField] private string lastSpeaker;

    [Header("Dialogue Settings")]
    [SerializeField] private float delay;
    [SerializeField] private TextAsset currentScene;
    [SerializeField] private string nextScene;
    [SerializeField] private bool finalScene;


    private Story currentConvo;
    private bool dialogueIsPlaying;
    private bool timerEnabled;
    private float timer;


    private const string SPEAKER_TAG = "speaker";


    void Start()
    {
        DisableDialogue(string.Empty, string.Empty);
        timerEnabled = false;
        StartDialogue(currentScene);
    }

    private void Awake()
    {
        if (instance != null) //warns if there is ever more than one of a dialogue manager
        {
            Debug.LogWarning("Multiple of these, which is bad apparently.");
        }
        instance = this;
    }

    void Update()
    {
        if (!dialogueIsPlaying) //we only want to update while there is dialogue, so return right away if dialogue isnt playing
        {
            return;
        }

        if (timerEnabled)
        {
            timer += Time.deltaTime;
        }

        // handles getting to the next line of dialogue when submit is pressed. However we will need to mess with this because we are having 4 separate ways of displaying dialogue and may only want to press submit for some of them. 
        if (Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }
    }

    public void StartDialogue(TextAsset inkJSON) //Temp. This will start the dialogue but I will need to add if statesments within using Ink tags to decide where the dialogue actually goes.
    {
        currentConvo = new Story(inkJSON.text); //sets the current story
        dialogueIsPlaying = true; //makes sure we know dialogue is playing

        currentConvo.Continue(); //skips the first line making sure that the tags there are ready to be read

    }

    public static DialogueController GetInstance() //does the thing part two
    {
        return instance;
    }

    private void ContinueStory()
    {
        if (currentConvo.canContinue) //add checks for tags here before displaying the next text
        {
            HandleTags(currentConvo.currentTags);
            EnableDialogue(nextSpeaker);

            if (nextSpeaker == "Box")
            {
                    boxText.text = currentConvo.Continue();
            }
            else if (nextSpeaker == "Spike")
            {
                    spikeText.text = currentConvo.Continue();
            }
            else if (nextSpeaker == "Flower")
            {
                    flowerText.text = currentConvo.Continue();
            }
            else
            {
                    Debug.LogWarning("WARNING. The previous line was untagged and therefore a speaker cannot be assigned.");
            }
        }
        else if(finalScene == false)
        {
            NextScene();
        }
        else if (finalScene == true)
        {
            DisableBoxes();
            EndGame.GetInstance().EndTheGame();
        }
    }
    private void HandleTags(List<string> currentTags)
    {

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
                case SPEAKER_TAG:
                    Debug.Log("speaker=" + tagValue);
                    nextSpeaker = tagValue;
                    break;
                default:
                    Debug.LogWarning("Tag came in but cannot or is currently not being handled: " + tag);
                    break;
            }
        }
    }
    public void DisableDialogue(string currentSpeaker, string delaySpeaker)
    {
        timerEnabled = true;
        if(currentSpeaker == "Box") // this is probably WAY longer and more complicated than it needs to be but it works. Disables last speaker set by a delay in the editor
        {
            if (delaySpeaker == "Spike")
            {
                flowerHolder.SetActive(false);
                if(timer > delay)
                {
                    spikeHolder.SetActive(false);
                }
            }
            else if (delaySpeaker == "Flower")
            {
                spikeHolder.SetActive(false);
                if (timer > delay)
                {
                    flowerHolder.SetActive(false);
                }
            }
            else if (delaySpeaker == "Box")
            {
                flowerHolder.SetActive(false);
                spikeHolder.SetActive(false);
            }
            timerEnabled = false;
        }
        else if (currentSpeaker == "Spike")
        {
            if (delaySpeaker == "Box")
            {
                flowerHolder.SetActive(false);
                if (timer > delay)
                {
                    boxHolder.SetActive(false);
                }
            }
            else if (delaySpeaker == "Flower")
            {
                boxHolder.SetActive(false);
                if (timer > delay)
                {
                    flowerHolder.SetActive(false);
                }
            }
            else if (delaySpeaker == "Spike")
            {
                flowerHolder.SetActive(false);
                boxHolder.SetActive(false);
            }
            timerEnabled = false;
        }
        else if (currentSpeaker == "Flower")
        {
            if (delaySpeaker == "Box")
            {
                spikeHolder.SetActive(false);
                if (timer > delay)
                {
                    boxHolder.SetActive(false);
                }
            }
            else if (delaySpeaker == "Spike")
            {
                boxHolder.SetActive(false);
                if (timer > delay)
                {
                    spikeHolder.SetActive(false);
                }
            }
            else if (delaySpeaker == "Flower")
            {
                spikeHolder.SetActive(false);
                boxHolder.SetActive(false);
            }
            timerEnabled = false;
        }
        else
        {
            timerEnabled = false;
            boxHolder.SetActive(false);
            flowerHolder.SetActive(false);
            spikeHolder.SetActive(false);
        }

    }

    private void EnableDialogue(string currentSpeaker)
    {
        DisableDialogue(currentSpeaker, lastSpeaker);
        if (currentSpeaker == "Box")
        {
            boxHolder.SetActive(true);
            lastSpeaker = currentSpeaker;
        }
        else if (currentSpeaker == "Spike")
        {
            spikeHolder.SetActive(true);
            lastSpeaker = currentSpeaker;
        }
        else if (currentSpeaker == "Flower")
        {
            flowerHolder.SetActive(true);
            lastSpeaker = currentSpeaker;
        }
    }

    private void NextScene()
    {
        SceneManager.LoadScene(sceneName: nextScene);
    }

    private void DisableBoxes()
    {
        boxHolder.SetActive(false);
        flowerHolder.SetActive(false);
        spikeHolder.SetActive(false);
    }
}
