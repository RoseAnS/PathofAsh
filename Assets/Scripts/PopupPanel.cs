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
    private int memNum;
    private int clickNum;
    private Story currentScenario;

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
            NextMemory();
            OpenPanel();
        }    

    }

    public void OpenPanel()
    {
        popUpObject.SetActive(true);
        ContinueStory();
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
        if (currentScenario.canContinue) //add checks for tags here before displaying the next text
        {
            scenarioText.text = scenarioText.text + currentScenario.Continue();
            StartCoroutine(ForceScrollDown()); //starts the autoscroller
        }
    }

    public void StartDialogue(TextAsset inkJSON) //Temp. This will start the dialogue but I will need to add if statesments within using Ink tags to decide where the dialogue actually goes.
    {
        currentScenario = new Story(inkJSON.text); //sets the current story

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
            selectedMemory = memory1;
            memNum++;
            StartDialogue(selectedMemory);
        }
        else if (memNum == 1)
        {
            selectedMemory = memory2;
            scenarioText.text = null;
            memNum++;
        }
        else if (memNum == 2)
        {
            selectedMemory = memory3;
            memNum++;
        }
    }
}
