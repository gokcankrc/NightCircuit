using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorsHandler : Singleton<IndicatorsHandler>
{

    public void FlushIndicators()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
