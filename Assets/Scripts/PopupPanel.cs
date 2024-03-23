using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description; 


   private static PopupPanel instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public static void Show(RootData rootData)
    {
        instance.title.text = rootData.Title;
        instance.description.text = rootData.Description;
        instance.gameObject.SetActive(true);
    }
}
