using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public float whenDoesFadingStart = 1;
    public float howMuchFadingLasts = 1;
    
    [SerializeField] private TextMeshProUGUI textMpComponent;

    void Start()
    {
        StartCoroutine(nameof(Fade));
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(whenDoesFadingStart);
        var fadingCurrent = howMuchFadingLasts;

        while (true)
        {
            fadingCurrent -= Time.deltaTime;
            textMpComponent.alpha = fadingCurrent / howMuchFadingLasts;
            
            if (fadingCurrent < 0)Destroy(gameObject);
            yield return null;
        }
    }

}
