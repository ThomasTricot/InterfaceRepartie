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
            }
            else
            {
                Debug.LogError("The guitare prefab does not have a RectTransform component.");
            }
        }
    }
}
