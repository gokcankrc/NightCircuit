using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllBatteries : MonoBehaviour
{
    public bool checkAllBatteries()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<BatteryPortScript>().CheckIfFull();
        }
        // foreach (var VARIABLE in COLLECTION)
        // {
            
        // }
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
