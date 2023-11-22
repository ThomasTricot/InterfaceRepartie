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
            // Instanciez le prefab de piano
            piano = GameObject.Find("pianoPrefab0");
            if (piano == null)
            {
                Debug.LogError("pianoPrefab not found in the scene");
                return;
            }

            // Assurez-vous que l'objet instancié utilise un RectTransform
            RectTransform pianoRectTransform = piano.GetComponent<RectTransform>();
            if (pianoRectTransform != null)
            {
                // Positionnez le piano en (0, 0) localement dans le canvas
                pianoRectTransform.localPosition = Vector2.zero;
            }
            else
            {
                Debug.LogError("The piano prefab does not have a RectTransform component.");
            }
        }
    }
}