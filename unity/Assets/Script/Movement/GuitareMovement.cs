using UnityEngine;
using Script;

public class GuitareMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject guitare;
    
    private int guitareId = 1;



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
        Vector2 position = OSC.GetInstrumentPosition(guitareId);

        guitare = ObjectFinder.FindGuitarePrefab();
        if (position != Vector2.zero && guitare == null)
        {
            InstrumentSpawner.SpawnGuitareAt(CoordConvertor.Convert(position[0], position[1]));
        }

        
        if (guitare != null)
        {
            RectTransform guitareRectTransform = guitare.GetComponent<RectTransform>();
            if (guitareRectTransform != null)
            {
                guitareRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]);
                AdjustVolumeBasedOnDistance();
            }
            else
            {
                Debug.LogError("The guitare prefab does not have a RectTransform component.");
            }
        }
    }

    void AdjustVolumeBasedOnDistance()
    {
        GameObject BpmPrefab = GameObject.Find("BpmPrefab1");
        if (BpmPrefab == null || guitare == null)
        {
            Debug.LogError("BpmPrefab1 or Battery is null");
            return;
        }

        float distance = Vector2.Distance(guitare.transform.position, BpmPrefab.transform.position);
        AudioSource audioSource = guitare.GetComponent<AudioSource>();
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
                // Interpolation lin�aire entre minVolume et maxVolume
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
