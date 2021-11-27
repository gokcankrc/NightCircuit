using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArrowKeysIndicator : MonoBehaviour
{
    private SpriteRenderer sprite;
    [FormerlySerializedAs("Key")] [SerializeField] private KeyCode key;
    [SerializeField] private Color colorIdle;
    [SerializeField] private Color colorPress;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(key)) sprite.color = colorPress;
        else sprite.color = colorIdle;
    }
}
