using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class PopupPanel : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private TextAsset selectedMemory;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scenarioText;
    [SerializeField] private ScrollRect scrollView;


    private Story currentScenario;

    private static PopupPanel instance;


    public static PopupPanel GetInstance() //does the thing part two
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void OpenPanel()
    {
        Debug.Log("you have been clicked");
        StartDialogue(selectedMemory);
        gameObject.SetActive(true);
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
            scenarioText.text = currentScenario.Continue();
            StartCoroutine(ForceScrollDown()); //starts the autoscroller
        }
    }

    public void StartDialogue(TextAsset inkJSON) //Temp. This will start the dialogue but I will need to add if statesments within using Ink tags to decide where the dialogue actually goes.
    {
        currentScenario = new Story(inkJSON.text); //sets the current story
        ContinueStory();

    }
    IEnumerator ForceScrollDown()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        scrollView.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

}
