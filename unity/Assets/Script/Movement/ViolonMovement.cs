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
            Debug.LogError("BpmPrefab1 or violon is null");
            return;
        }

        float distance = Vector2.Distance(violon.transform.position, BpmPrefab.transform.position);
        AudioSource audioSource = violon.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            float minDistance = 60.0f; // La distance pour le volume maximal
            float maxDistance = 250.0f; // La distance pour le volume minimal
            float minVolume = 0.1f; // Le volume minimal
            float maxVolume = 1.0f; // Le volume maximal

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
            Debug.LogError("AudioSource is missing on the violon");
        }
    }

}
