using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBatteries : Singleton<PlaceBatteries>
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float batteryMaxDist;
    
    [SerializeField] public int currentBatteries;
    

    private void Update()
    {
        if (GameDebugMode.DebugMode && Input.GetKeyDown(KeyCode.F3))
        {
            currentBatteries += 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // check and do either of these.
            if (!AttemptPortBatteryExchange())  // if completely failed Port exchange
            {
                AttemptRemoveBattery();  // take a naked battery
            }
        }
            
        // Debug.Log(GameStateManager.State);
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            Debug.Log(GameStateManager.State);
            switch (GameStateManager.State)
            {
                case "LightsOn":
                    MessageManager.I.Notify("You put all the batteries back in their place");
                    BatteriesHandler.I.FlushBatteries();
                    currentBatteries = 0;
                    break;
            }
        }
    }


    private bool RaycastAtCrosshair(out RaycastHit hit, LayerMask layerMask)
    {
        var position = cameraTransform.position;
        // var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // If only i had a mouse
        Ray ray = new Ray(position, cameraTransform.forward);
        return Physics.Raycast(ray, out hit, batteryMaxDist, layerMask);
        
    }

    private bool AttemptPortBatteryExchange()
    {
        // Raycast the Ports, return if none or too far
        RaycastHit hit;
        if (!RaycastAtCrosshair(out hit, LayerMask.GetMask("Battery Port"))) return false; // no Port
        if (!(hit.distance < batteryMaxDist)) return false;  // return if target is too far

        var port = hit.transform.parent.GetComponent<BatteryPortScript>();
        
        if (currentBatteries <= 0 && port.BatteriesCount == 0)
        {
            MessageManager.I.Notify("You have no batteries");  // only applies if we hit a Port without a battery
            return false;
        }
        if (currentBatteries <= 0 && port.BatteriesCount > 0)
        {
            MessageManager.I.Notify("You took the batteries from battery port");
            currentBatteries += port.RemoveAllBattery();
            return true;
        }
        if (currentBatteries > 0 && port.maxBatteryAmount > port.BatteriesCount)
        {
            currentBatteries -= 1;
            port.PlaceBattery();  // place one more battery in there.
            return true;
        }

        if (currentBatteries > 0 && port.maxBatteryAmount == port.BatteriesCount)
        {
            currentBatteries += port.RemoveAllBattery();
            return true;
        }

        throw new Exception("Port battery exchange attempt has combusted in thin air");
    }
    
    private void AttemptRemoveBattery()
    {
        // Raycast, mask is only batteries
        RaycastHit hit;
        if (RaycastAtCrosshair(out hit, LayerMask.GetMask("Battery")))
        {
            currentBatteries += 1;
            Destroy(hit.transform.parent.gameObject);
        }
    }
}
