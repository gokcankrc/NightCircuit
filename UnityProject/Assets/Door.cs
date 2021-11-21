using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool doorIsOpen = false;
    
    [SerializeField] private GameObject slidingDoor1;
    [SerializeField] private Transform slidingPos1_1;
    [SerializeField] private Transform slidingPos1_2;
    private Vector3 _desiredPos1;
    
    [SerializeField] private GameObject slidingDoor2;
    [SerializeField] private Transform slidingPos2_1;
    [SerializeField] private Transform slidingPos2_2;
    private Vector3 _desiredPos2;

    [SerializeField] private float _slideSpeed;
    
    public void OpenDoor(int dir=0)
    {
        if (dir != 0)
        {
            // special case by "BatteryPortScrirt"
            if ((dir == 1 && !doorIsOpen) || (dir == -1 && doorIsOpen))
            {
                OpenDoor();
            }
            return;
        }
        if (!doorIsOpen)
        {
            _desiredPos1 = slidingPos1_2.position;
            _desiredPos2 = slidingPos2_2.position;
            doorIsOpen = true;
        }
        else
        {
            _desiredPos1 = slidingPos1_1.position;
            _desiredPos2 = slidingPos2_1.position;
            doorIsOpen = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // ALMOST AUTO GENERATED
        // WORST AND SLOPPIEST AND DIRTIEST SLIDING DOOR ANIMATION
        // DONE IN LIKE 10 MINUTES
        // YOU MIGHT GET AN STD AND SOME MORE DISEASES AND ALSO CANCER
        Vector3 position1;
        Vector3 position2;
        
        position1 = slidingDoor1.transform.position;
        position2 = slidingDoor2.transform.position;
            
        _desiredPos1 = position1;
        _desiredPos2 = position2;

        slidingPos1_1.position = position1;
        slidingPos2_1.position = position2;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDebugMode.DebugMode && Input.GetKeyDown(KeyCode.F4))
        {
            OpenDoor();
        }
        
        var dir = _desiredPos1 - slidingDoor1.transform.position;
        dir = dir.normalized;
        slidingDoor1.transform.position += dir * _slideSpeed;
        
        dir = _desiredPos2 - slidingDoor2.transform.position;
        dir = dir.normalized;
        slidingDoor2.transform.position += dir * _slideSpeed;
    }
}
