using Script;
using UnityEngine;

public class ValiderMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject valider;
    
    private int validerId = 6;



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
        Vector2 position = OSC.GetInstrumentPosition(validerId);
        
        valider = GameObject.Find("validatePrefab0");
        if (position != Vector2.zero && valider == null)
        {
            InstrumentSpawner.SpawnValidateAt(CoordConvertor.Convert(position[0], position[1]));
        }

        
        if (valider != null)
        {
            RectTransform validateRectTransform = valider.GetComponent<RectTransform>();
            if (validateRectTransform != null)
            {
                validateRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]);
            }
            else
            {
                Debug.LogError("The validate prefab does not have a RectTransform component.");
            }
        }
    }
}
