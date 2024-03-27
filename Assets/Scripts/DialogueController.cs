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

    private Story currentConvo;
    private bool dialogueIsPlaying;


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
            boxText.text = currentConvo.Continue();
        }
    }
}
