using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Runtime.CompilerServices;

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
    [SerializeField] private string dialogueOutputLocation;

    private Story currentConvo;
    private bool dialogueIsPlaying;

    private const string SPEAKER_TAG = "speaker";


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
        ContinueStory();

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

            if (dialogueOutputLocation == "Box")
            {
                    boxText.text = currentConvo.Continue();
            }
            else if (dialogueOutputLocation == "Spike")
            {
                    spikeText.text = currentConvo.Continue();
            }
            else if (dialogueOutputLocation == "Flower")
            {
                    flowerText.text = currentConvo.Continue();
            }
            else
            {
                    Debug.LogWarning("WARNING. The previous line was untagged and therefore a speaker cannot be assigned.");
            }
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
                    dialogueOutputLocation = tagValue;
                    break;
                default:
                    Debug.LogWarning("Tag came in but cannot or is currently not being handled: " + tag);
                    break;
            }
        }
    }
}
