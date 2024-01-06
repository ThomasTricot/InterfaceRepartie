using Script;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class PianoMoving : MonoBehaviour
{
    private Canvas canvas;

    private GameObject piano;


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
        float x = OSC.StaticX;
        float y = OSC.StaticY;
        
        piano = GameObject.Find("pianoPrefab0");

        if (x != 0 && y != 0 && piano == null)
        {
            InstrumentSpawner.SpawnPianoAt(CoordConvertor.Convert(x, y));
        }
        
        if (piano == null)
        {
            // Debug.LogError("pianoPrefab not found in the scene");
            return;
        }

        RectTransform pianoRectTransform = piano.GetComponent<RectTransform>();
        if (pianoRectTransform != null)
        {
            pianoRectTransform.localPosition = CoordConvertor.Convert(x, y);
        }
        else
        {
            Debug.LogError("The piano prefab does not have a RectTransform component.");
        }
    }
}