using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPurposeAudio : MonoBehaviour
{
    [Tooltip("Optional. If is undefined, finds the AudioSource component in the current GameObject.")]
    [SerializeField] private AudioSource audioSource;

    private float defaultAudio;
    
    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        
        GameStateManager.ONAudioLevelChange += ChangeAudioLevel;
        
        defaultAudio = audioSource.volume;
        ChangeAudioLevel(GameStateManager.I.AudioLevelNormalized);
    }

    void ChangeAudioLevel(float volume)
    {
        audioSource.volume = defaultAudio * volume;
    }
}
