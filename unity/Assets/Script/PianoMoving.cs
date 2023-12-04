using Script;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            piano = GameObject.Find("pianoPrefab0");
            if (piano == null)
            {
                Debug.LogError("pianoPrefab not found in the scene");
                return;
            }

            RectTransform pianoRectTransform = piano.GetComponent<RectTransform>();
            if (pianoRectTransform != null)
            {

                pianoRectTransform.localPosition = CoordConvertor.Convert(0.25f, 0.75f);
            }
            else
            {
                Debug.LogError("The piano prefab does not have a RectTransform component.");
            }
        }
    }
}