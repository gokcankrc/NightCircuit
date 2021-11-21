using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivaterOnStart : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> allCeiling;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var ceilingGo in allCeiling.Where(ceilingGo => ceilingGo != null))
        {
            ceilingGo.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
