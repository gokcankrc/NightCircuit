using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceIndicators : Singleton<PlaceIndicators>
{
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private Transform indicatorsFolder;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private Transform level1Checkpoint;
    [SerializeField] public Transform level2Checkpoint;

    [NonSerialized] public Transform lastCheckpoint;

    [SerializeField] private int maxIndicators;
    [SerializeField] private float indicatorMaxDist;
    
    [SerializeField] public int currentIndicators;
    
    [SerializeField] public GameObject placeIndicatorSoundEffect;
    


    private void Start()
    {
        lastCheckpoint = level1Checkpoint;
        ResetIndicators();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttemptPlaceIndicator();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AttemptRemoveIndicator();
        }
        
        // Debug.Log(GameStateManager.State);
        if (Input.GetKeyDown(KeyCode.R))
        {
            MessageManager.I.Notify("You reset back to checkpoint.");
            ResetIndicators();
            IndicatorsHandler.I.FlushIndicators();
            transform.localPosition = lastCheckpoint.localPosition;
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (GameStateManager.state)
            {
                case "LightsOff":
                    MessageManager.I.Notify("You cannot take the indicators when the light is off. Press \"R\" to reload from the last checkpoint.");
                    break;
                case "LightsOn":
                    MessageManager.I.Notify("You took the indicators back.");
                    ResetIndicators();
                    IndicatorsHandler.I.FlushIndicators();
                    // transform.localPosition = lastCheckpoint.localPosition;
                    break;
            }
        }
    }

    public void ResetIndicators()
    {
        currentIndicators = maxIndicators;
    }

    private bool RaycastAtCrosshair(out RaycastHit hit, LayerMask layerMask)
    {
        var position = cameraTransform.position;
        // var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // If only i had a mouse
        Ray ray = new Ray(position, cameraTransform.forward);
        return Physics.Raycast(ray, out hit, indicatorMaxDist, layerMask);
        
    }

    private void AttemptPlaceIndicator()
    {
        if (currentIndicators <= 0)
        {
            MessageManager.I.Notify("You ran out of indicators");
            return;
        }

        // if wall is close enough, place indicator to the wall.
        
        // Raycast
        RaycastHit hit;
        if (!RaycastAtCrosshair(out hit, LayerMask.GetMask("Wall"))) return;
        if (!(hit.distance < indicatorMaxDist)) return;  // return if target is too far
        
        //hit is successful. proceed
        var freshIndicatorTransform = Instantiate(indicatorPrefab, indicatorsFolder).transform;
        Instantiate(placeIndicatorSoundEffect, transform);
        currentIndicators -= 1;
                
        // Set position and rotation
        Vector3 indicatorPlacementVector = cameraTransform.position + cameraTransform.forward * hit.distance;
        freshIndicatorTransform.position = indicatorPlacementVector;
        freshIndicatorTransform.LookAt(freshIndicatorTransform.position + hit.normal);
    }
    
    private void AttemptRemoveIndicator()
    {
        // Raycast
        RaycastHit hit;
        if (!RaycastAtCrosshair(out hit, LayerMask.GetMask("Indicators"))) return;
        
        // collide with TargetCollision of indicator gameobject. If it does collide, remove the parent of it, which is the indicator daddy atm.
        Destroy(hit.transform.parent.gameObject);
        currentIndicators += 1;
    }
}
