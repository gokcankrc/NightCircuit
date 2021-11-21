using System;
using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class LightSwitch : Singleton<LightSwitch>
{
    [SerializeField] private GameObject[] lightContainers;

    public void FlickThaSwitch(int level, int flickDirection=0)
    {
        // flick dir is -1 if closing all the lights, and 1 if opening all. 0 for not (activeness), but don't use for debug purposes.
        
        var lightContainer = transform.GetChild(level).gameObject;
        switch (flickDirection)
        {
            case -1:
                lightContainer.SetActive(false);
                break;
            case 0:
                lightContainer.SetActive(!lightContainer.activeSelf);
                break;
            case 1:
                lightContainer.SetActive(true);
                break;
        }
    }

    void Update()
    {
        if (GameDebugMode.DebugMode)
        {
            if (Input.GetKeyDown(KeyCode.F1)) FlickThaSwitch(0);
            if (Input.GetKeyDown(KeyCode.F2)) FlickThaSwitch(1);
        }
    }
}
