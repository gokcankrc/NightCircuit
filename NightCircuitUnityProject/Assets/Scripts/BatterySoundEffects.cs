using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySoundEffects : MonoBehaviour
{
    [SerializeField] private GameObject onSound;
    [SerializeField] private GameObject offSound;
    [SerializeField] private bool soundsOrNot = true;

    public void PlayOnSound()
    {
        if (!soundsOrNot) return;
        var _sound = Instantiate(onSound);
        _sound.transform.parent = transform;
        _sound.transform.position = transform.position;

    }

    public void PlayOffSound()
    {
        if (!soundsOrNot) return;
        var _sound = Instantiate(offSound);
        _sound.transform.parent = transform;
        _sound.transform.position = transform.position;

    }
}
