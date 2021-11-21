using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>
{
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform notificationMessagesTransform;
    [SerializeField] private TextMeshProUGUI amountsDisplay;
    [SerializeField] private Vector2 messageDisplaceVector;
    [SerializeField] private Vector2 messageOffset;
    [SerializeField] private Vector2 messageBox;
    [SerializeField] private List<string> InitialTexts = new List<string>();

    public int level = 1;

    private void Start()
    {
        InitialTexts.Add("Batteries:");
        InitialTexts.Add("");
        InitialTexts.Add("\nIndicators left:");
        InitialTexts.Add("");
        InitialTexts.Add("\nLevel:");
        InitialTexts.Add("");
    }

    private void FixedUpdate()
    {
        UpdateAmountsDisplay();
    }

    public void Notify(string messageString)
    {
        var currentMessage = Instantiate(messagePrefab, notificationMessagesTransform);
        currentMessage.GetComponent<TextMeshProUGUI>().text = messageString;
        
        // displace for them to not stack
        RectTransform messsageRect = currentMessage.GetComponent<RectTransform>();
        
        messsageRect.anchoredPosition =  new Vector2(messageDisplaceVector.x, rectTransform.offsetMax.y + messageDisplaceVector.y);
        messsageRect.sizeDelta = messageBox;
        
        for (int j = 0; j < notificationMessagesTransform.childCount; j++)
        {
            notificationMessagesTransform.GetChild(j).GetComponent<RectTransform>().anchoredPosition += messageOffset;
        }
    }

    public void UpdateAmountsDisplay()
    {
        InitialTexts[1] = PlaceBatteries.I.currentBatteries.ToString();
        InitialTexts[3] = PlaceIndicators.I.currentIndicators.ToString();
        InitialTexts[5] = level.ToString();
        string combinedText = "";
        foreach (var InitialTextMember in InitialTexts)
        {
            combinedText += InitialTextMember;
        }
        amountsDisplay.text = combinedText;
    }
}
