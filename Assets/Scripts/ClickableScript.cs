using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ClickableScript : MonoBehaviour, IPointerClickHandler
{

    private string checkData;
    private int boxCheck;
    private int flowerCheck;
    private int spikeCheck;
    private string keyHold;

    private bool boxConnect;
    private bool flowerConnect;
    private bool spikeConnect;


    void Start()
    {
        boxConnect = false;
        flowerConnect = false;
        spikeConnect = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        var text = GetComponent<TextMeshProUGUI>();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex > -1)
            {
                var linkInfo = text.textInfo.linkInfo[linkIndex];
                var linkId = linkInfo.GetLinkID();

                var rootData = FindAnyObjectByType<RootDataController>().Get(linkId);


               PopupPanel.Show(rootData);
                Checker(rootData);
            }
        }
    }

    private void Checker(RootData rootData)
    {
        if (rootData.Key != keyHold)
        {
            keyHold = rootData.Key;
            if(rootData.Check == "box")
            {
                if (boxCheck >= 3)
                {
                    boxConnect = true;
                }
                else
                {
                    boxCheck = boxCheck + 1;
                }
            }
            if (rootData.Check == "flower")
            {
                if (flowerCheck >= 3)
                {
                    flowerConnect = true;
                }
                else
                {
                    flowerCheck = flowerCheck + 1;
                }
            }
            if (rootData.Check == "spike")
            {
                if (spikeCheck >= 3)
                {
                    spikeConnect = true;
                }
                else
                {
                    spikeCheck = spikeCheck + 1;
                }
            }
        }
    }
    public void CheckSender()
    {
        Debug.Log("check");
        ScenarioMaster.GetInstance().InkCheckTracker(boxConnect, flowerConnect, spikeConnect);
    }
}
