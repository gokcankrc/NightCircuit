using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static string state = "LightsOn";  // Either "LightsOn" or "LightsOff"
    public static int level = 1;  // Either "LightsOn" or "LightsOff"
    public a level2;  // Either "LightsOn" or "LightsOff"
    
    
    void Start()
    {
        state = "LightsOn";
    }

    void MightHaveTurnedLightsOff()
    {
        // what level are we at?
        
            
    }
}


//
// [CreateAssetMenu(menuName = "CharacterData")]
// public class Character : ScriptableObject
// {
//     [Tooltip("Character's real name")]
//     public string characterName = "Berkecan Smith";
//     public Sprite sprite;
//     [Tooltip("The way character greets/introduces themselves")]
//     public Dialogue startingDialogue;
//     [Tooltip("Askable question")]
//     public Dialogue questions;
//     [Tooltip("Corresponding answer")]
//     public Dialogue[] answers;
// }

public class a : ScriptableObject
{
    public a()
    {
        c = 2;
        d = 54;
    }
    public int c;
    public int d;
}
