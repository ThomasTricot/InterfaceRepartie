using Script;
using UnityEngine;

public class PianoMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject piano;
    
    private int pianoId = 0;



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
        
        Vector2 position = OSC.GetInstrumentPosition(pianoId);

        piano = ObjectFinder.FindPianoPrefab();
        if (position != Vector2.zero && piano == null)
        {
            InstrumentSpawner.SpawnPianoAt(CoordConvertor.Convert(position[0], position[1]));
        }
        
        if (piano != null)
        {
            RectTransform pianoRectTransform = piano.GetComponent<RectTransform>();
            if (pianoRectTransform != null)
            {
                pianoRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]);
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
        if (BpmPrefab == null || piano == null)
        {
            Debug.LogError("BpmPrefab1 or piano is null");
            return;
        }

        float distance = Vector2.Distance(piano.transform.position, BpmPrefab.transform.position);
        AudioSource audioSource = piano.GetComponent<AudioSource>();
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
            Debug.LogError("AudioSource is missing on the piano");
        }
    }


}