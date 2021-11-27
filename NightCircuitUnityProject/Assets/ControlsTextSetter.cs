using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlsTextSetter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private List<string> textList;
    
    // Start is called before the first frame update
    void Start()
    {
        return;
        var totalText = "";
        foreach (var txt in textList)
        {
            totalText += txt;
        }
        textMeshProUGUI.text = totalText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
