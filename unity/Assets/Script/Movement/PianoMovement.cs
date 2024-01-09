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
        
        piano = GameObject.Find("pianoPrefab0");
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
            }
            else
            {
                Debug.LogError("The piano prefab does not have a RectTransform component.");
            }
        }
    }
}