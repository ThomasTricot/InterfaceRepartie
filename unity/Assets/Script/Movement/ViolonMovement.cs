using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script;


public class ViolonMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject violon;
    
    private int violonId = 3;



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
        Vector2 position = OSC.GetInstrumentPosition(violonId);

        violon = ObjectFinder.FindViolonPrefab();
        if (position != Vector2.zero && violon == null)
        {
            InstrumentSpawner.SpawnViolonAt(CoordConvertor.Convert(position[0], position[1]));
        }

        
        if (violon != null)
        {
            RectTransform violonRectTransform = violon.GetComponent<RectTransform>();
            if (violonRectTransform != null)
            {
                violonRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]);
                AdjustVolumeBasedOnDistance();
            }
            else
            {
                Debug.LogError("The piano prefab does not have a RectTransform component.");
            }
        }
    }

    void AdjustVolumeBasedOnDistance()
    {
        GameObject BpmPrefab = GameObject.Find("BpmPrefab1");
        if (BpmPrefab == null || violon == null)
        {
            Debug.LogError("BpmPrefab1 or Battery is null");
            return;
        }

        float distance = Vector2.Distance(violon.transform.position, BpmPrefab.transform.position);
        AudioSource audioSource = violon.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            float minDistance = 60.0f;
            float maxDistance = 250.0f;
            float minVolume = 0.2f;
            float maxVolume = 1.0f;

            if (distance <= minDistance)
            {
                audioSource.volume = minVolume;
            }
            else if (distance >= maxDistance)
            {
                audioSource.volume = maxVolume;
            }
            else
            {
                // Interpolation linéaire entre minVolume et maxVolume
                float t = (distance - minDistance) / (maxDistance - minDistance);
                audioSource.volume = Mathf.Lerp(minVolume, maxVolume, t);
            }

            Debug.Log($"Distance: {distance}, Volume: {audioSource.volume}");
        }
        else
        {
            Debug.LogError("AudioSource is missing on the battery");
        }
    }
}
