using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public string state = "LightsOn";  // Either "LightsOn" or "LightsOff"
    public int level = 1;
    
    [Range(0, 100)] public int audioLevel = 50;  // out of hundred
    public float AudioLevelNormalized => audioLevel * 2f / 100f;  // defaults are 50, and we should allow players to go higher from 50.
    [SerializeField] private int audioLevelIncement = 5;

    [SerializeField] public KeyCode musicUpKey;
    [SerializeField] public KeyCode musicDownKey;

    public delegate void AudioLevelChange(float audioLevelNormalized);
    public static event AudioLevelChange ONAudioLevelChange ;

    public void VolumeChange(int change)
    {
        audioLevel = Mathf.Clamp(audioLevel + change, 0, 100);
        ONAudioLevelChange?.Invoke(AudioLevelNormalized);
        MessageManager.I.Notify("Volume: " + audioLevel);
    }

    void Awake()
    {
        state = "LightsOn";
    }

    private void Start()
    {
        ONAudioLevelChange?.Invoke(AudioLevelNormalized);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(musicDownKey)) VolumeChange(-audioLevelIncement);
        if (Input.GetKeyDown(musicUpKey)) VolumeChange(audioLevelIncement);
        if (Input.GetKeyDown(KeyCode.R)) state = "LightsOn";
    }
    
    
    
    
    
    
    
    
    
    
    public a level2;
    public a level32;
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
