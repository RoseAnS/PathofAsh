using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class PopupPanel : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private TextAsset memory1;
    [SerializeField] private TextAsset memory2;
    [SerializeField] private TextAsset memory3;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scenarioText;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private GameObject popUpObject;

    private TextAsset selectedMemory;
    public int memNum;
    private int clickNum;
    private Story memoryStory1;
    private Story memoryStory2;
    private Story memoryStory3;


    private static PopupPanel instance;


    public static PopupPanel GetInstance() //does the thing part two
    {
        return instance;
    }
    private void Awake()
    {
        memNum = 0;
        instance = this;
        popUpObject.SetActive(false);
        clickNum = 0;
    }

    public void Count()
    {
        if (clickNum < 2)
        {
            clickNum++;
        }
        else if (clickNum == 2)
        {
            clickNum = 0;
            NextMemory();
            OpenPanel();
        }    

    }
    void Start()
    {
        StartDialogue(memory1, memory2, memory3);
    }

    public void OpenPanel()
    {
        popUpObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }
    }
    private void ContinueStory()
    {
        if (memNum == 1)
        {
            if (memoryStory1.canContinue) //add checks for tags here before displaying the next text
            {
                scenarioText.text = scenarioText.text + memoryStory1.Continue();
                StartCoroutine(ForceScrollDown()); //starts the autoscroller
            }
        }
        else if (memNum == 2)
        {
            if (memoryStory2.canContinue) //add checks for tags here before displaying the next text
            {
                scenarioText.text = scenarioText.text + memoryStory2.Continue();
                StartCoroutine(ForceScrollDown()); //starts the autoscroller
            }
        }
        else if (memNum == 3)
        {
            if (memoryStory3.canContinue) //add checks for tags here before displaying the next text
            {
                scenarioText.text = scenarioText.text + memoryStory3.Continue();
                StartCoroutine(ForceScrollDown()); //starts the autoscroller
            }
        }

    }

    public void StartDialogue(TextAsset inkJSON1, TextAsset inkJSON2,TextAsset inkJSON3) //Temp. This will start the dialogue but I will need to add if statesments within using Ink tags to decide where the dialogue actually goes.
    {
        Debug.Log("check");
        memoryStory1 = new Story(inkJSON1.text); //sets the current story
        memoryStory2 = new Story(inkJSON2.text);
        memoryStory3 = new Story(inkJSON3.text);
    }
    IEnumerator ForceScrollDown()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        scrollView.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    public void NextMemory()
    {
        if (memNum == 0)
        {
            memNum++;
            clickNum = 0;
        }
        else if (memNum == 1)
        {
            memNum++;
            scenarioText.text = null;
            clickNum = 0;
        }
        else if (memNum == 2)
        {
            memNum++;
            scenarioText.text = null;
            clickNum = 0;
        }
    }
}
