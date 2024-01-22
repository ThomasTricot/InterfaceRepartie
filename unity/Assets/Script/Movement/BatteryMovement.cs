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
        battery = ObjectFinder.FindBatteryPrefab();

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
                AdjustVolumeBasedOnDistance();
            }
            else
            {
                Debug.LogError("The battery prefab does not have a RectTransform component.");
            }
        }
    }

    void AdjustVolumeBasedOnDistance()
    {
        GameObject BpmPrefab = GameObject.Find("BpmPrefab1");
        if (BpmPrefab == null || battery == null)
        {
            Debug.LogError("BpmPrefab1 or Battery is null");
            return;
        }

        float distance = Vector2.Distance(battery.transform.position, BpmPrefab.transform.position);
        AudioSource audioSource = battery.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            float minDistance = 60.0f; // La distance pour le volume maximal
            float maxDistance = 250.0f; // La distance pour le volume minimal
            float minVolume = 1.0f; // Le volume minimal
            float maxVolume = 3.0f; // Le volume maximal

            if (distance <= minDistance)
            {
                audioSource.volume = maxVolume;
            }
            else if (distance >= maxDistance)
            {
                audioSource.volume = minVolume;
            }
            else
            {
                // Interpolation linéaire entre maxVolume et minVolume
                float t = (distance - minDistance) / (maxDistance - minDistance);
                audioSource.volume = Mathf.Lerp(maxVolume, minVolume, t);
            }

            Debug.Log($"Distance: {distance}, Volume: {audioSource.volume}");
        }
        else
        {
            Debug.LogError("AudioSource is missing on the battery");
        }
    }







}
