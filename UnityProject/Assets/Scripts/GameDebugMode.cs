using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDebugMode : Singleton<GameDebugMode>
{
    public static bool DebugMode { get; private set; }
    [SerializeField] private Color ambientColor1 = Color.black;
    [SerializeField] private Color ambientColor2;

    [SerializeField] private List<GameObject> allCeiling;

    private void Update()
    {
        DebugMode = Input.GetKeyDown(KeyCode.F9)?  !DebugMode : DebugMode;

        DebugStuff();
        
        
        

    }

    private void DebugStuff()
    {
        if (!DebugMode) return;
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            RenderSettings.ambientLight = RenderSettings.ambientLight == ambientColor1 ? ambientColor2 : ambientColor1;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            foreach (var ceilingGo in allCeiling.Where(ceilingGo => ceilingGo != null))
            {
                ceilingGo.SetActive(!ceilingGo.activeSelf);
            }
        }
    }
}
