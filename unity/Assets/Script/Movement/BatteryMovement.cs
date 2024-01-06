using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class BatteryMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject battery;
    
    private int batteryId = 2;



    void Start()
    {
        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas component not found on the GameObject");
        }
        
    }

    void Update()
    {
        Vector2 position = OSC.GetInstrumentPosition(batteryId);
        
        battery = GameObject.Find("batteryPrefab0");
        if (position != Vector2.zero && battery == null)
        {
            InstrumentSpawner.SpawnBatteryAt(CoordConvertor.Convert(position[0], position[1]));
        }

        
        if (battery != null)
        {
            RectTransform batteryRectTransform = battery.GetComponent<RectTransform>();
            if (batteryRectTransform != null)
            {
                batteryRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]);
            }
            else
            {
                Debug.LogError("The battery prefab does not have a RectTransform component.");
            }
        }
    }
}
