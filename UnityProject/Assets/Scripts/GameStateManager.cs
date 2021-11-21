using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static string State = "LightsOn";  // Either "LightsOn" or "LightsOff"
    
    void Start()
    {
        State = "LightsOn";
    }
}
