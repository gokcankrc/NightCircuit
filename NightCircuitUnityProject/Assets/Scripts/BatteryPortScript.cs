using System;
using UnityEngine;

public class BatteryPortScript : MonoBehaviour
{
    [SerializeField] public int maxBatteryAmount; 
    [SerializeField] private int defaultBatteries;
    [NonSerialized] public int BatteriesCount;
    [SerializeField] public int connectedLightsArray = -1;

    public bool IsFull => BatteriesCount == maxBatteryAmount;

    [SerializeField] private GameObject visualBattery;
    
    [SerializeField] private Material visualBatteryOnEmission;
    [SerializeField] private Color visualBatteryOnEmissionColor;
    [SerializeField] private Material visualBatteryOffEmission;
    [SerializeField] private Color visualBatteryOffEmissionColor;
    
    [SerializeField] private GameObject visualBatteryLightOnGameObject;
    [SerializeField] private GameObject visualBatteryLightOffGameObject;
    
    
    [SerializeField] private Light visualBatteryOnLight;
    [SerializeField] private Color visualBatteryOnLightColor;
    [SerializeField] private Color visualBatteryOffLightColor;
    
    [SerializeField] private Door connectedDoor;
    [SerializeField] private BatteryPortScript connectedDoorPort;
    [SerializeField] private BatteryPortScript connectedDoorPort2;
    
    [SerializeField] private BatteryPortScript lvl1BatterySwitch;

    [SerializeField] private BatterySoundEffects batterySoundEffects;

    private void ResetThis()
    {
        BatteriesCount = defaultBatteries;
        UpdateLightColors();
        UpdateLightContainers();
    }

    public bool CheckIfFull()
    {
        return BatteriesCount == maxBatteryAmount;
    }

    private void Start()
    {
        visualBatteryOnEmission.SetColor("_EmissionColor", visualBatteryOnEmissionColor);
        visualBatteryOffEmission.SetColor("_EmissionColor", visualBatteryOffEmissionColor);
        ResetThis();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetThis();
        }
    }

    private void SwitchActiveBatteryPortLight(bool activated)
    {
        // visualBatteryOnEmission.SetColor("_EmissionColor", visualBatteryOffEmissionColor);
        
        visualBatteryLightOnGameObject.SetActive(activated);
        visualBatteryLightOffGameObject.SetActive(!activated);
    }

    private void UpdateLightColors()
    {
        // play sounds
        
        if (BatteriesCount < maxBatteryAmount)
        {
            SwitchActiveBatteryPortLight(false);
            visualBatteryOnLight.color = visualBatteryOffLightColor;
            visualBattery.SetActive(false);
        }

        if (BatteriesCount == maxBatteryAmount)
        {
            SwitchActiveBatteryPortLight(true);
            visualBatteryOnLight.color = visualBatteryOnLightColor;
            visualBattery.SetActive(true);

        }
    }
    
    public void PlaceBattery()
    {
        BatteriesCount += 1;
        batterySoundEffects.PlayOnSound();
        UpdateLightColors();
        if (BatteriesCount >= maxBatteryAmount)
        {
            UpdateLightContainers();
            MessageManager.I.Notify("You successfully filled the battery port");
        }
    }

 
    private void UpdateLightContainers()
    {
        // flick dir is -1 if closing all the lights, and 1 if opening all. 0 for not (activeness), but don't use for debug purposes.
        switch (connectedLightsArray)
        {
            case -1:
                return;
            case -2:
                // special case: door
                if (connectedDoorPort.IsFull && IsFull) connectedDoor.OpenDoor(1);
                else connectedDoor.OpenDoor(-1);
                return;
            case -3:
                if (IsFull) connectedDoor.OpenDoor(1);
                else connectedDoor.OpenDoor(-1);
                return;
            case 0:
                GameStateManager.I.state = IsFull ? "LightsOn" : "LightsOff";
                break;
            case 1:
                if (IsFull && GameStateManager.I.level == 1)
                {
                    // level = 2
                    defaultBatteries = 1;
                    lvl1BatterySwitch.defaultBatteries = 0;
                    PlaceIndicators.I.lastCheckpoint = PlaceIndicators.I.level2Checkpoint;
                    GameStateManager.I.level = 2;
                    MessageManager.I.Notify("Checkpoint has reached");
                }

                if (GameStateManager.I.level == 2)
                {
                    GameStateManager.I.state = IsFull ? "LightsOn" : "LightsOff";
                }
                
                break;

            case -4:
                // special case: door
                if (connectedDoorPort.IsFull && connectedDoorPort2.IsFull && IsFull) connectedDoor.OpenDoor(1);
                else connectedDoor.OpenDoor(-1);
                return;
        }

        var flickDir = BatteriesCount;
        if (BatteriesCount < maxBatteryAmount) flickDir = -1;
        else if (BatteriesCount == maxBatteryAmount) flickDir = 1;

        LightSwitch.I.FlickThaSwitch(connectedLightsArray, flickDir);
    }


    public int RemoveAllBattery()
    {
        var amountToReturn = BatteriesCount;
        batterySoundEffects.PlayOffSound();
        BatteriesCount = 0;
        UpdateLightColors();
        UpdateLightContainers();
        return amountToReturn;
    }

}
