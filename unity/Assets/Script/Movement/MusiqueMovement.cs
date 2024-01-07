using Script;
using UnityEngine;

public class MusiqueMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject musique;
    
    private int musiqueId = 5;



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
        Vector2 position = OSC.GetInstrumentPosition(musiqueId);
        
        musique = GameObject.Find("musiquePrefab0");
        if (position != Vector2.zero && musique == null)
        {
            InstrumentSpawner.SpawnMusiqueAt(CoordConvertor.Convert(position[0], position[1]));
        }

        
        if (musique != null)
        {
            RectTransform musiqueRectTransform = musique.GetComponent<RectTransform>();
            if (musiqueRectTransform != null)
            {
                musiqueRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]);
            }
            else
            {
                Debug.LogError("The musique prefab does not have a RectTransform component.");
            }
        }
    }
}
