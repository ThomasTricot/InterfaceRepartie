using Script;
using UnityEngine;

public class ValiderTeacherMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject validerTeacher;

    private int validerTeacherId = 7;  // Modification de la variable ici

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
        Vector2 position = OSC.GetInstrumentPosition(validerTeacherId);  // Utilisation de validerTeacherId

        validerTeacher = GameObject.Find("validateTeacherPrefab0");
        if (position != Vector2.zero && validerTeacher == null)
        {
            InstrumentSpawner.SpawnValidateTeacherAt(CoordConvertor.Convert(position[0], position[1]));
        }


        if (validerTeacher != null)
        {
            RectTransform validateTeacherRectTransform = validerTeacher.GetComponent<RectTransform>();
            if (validateTeacherRectTransform != null)
            {
                validateTeacherRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]);
            }
            else
            {
                Debug.LogError("The validate prefab does not have a RectTransform component.");
            }
        }
    }
}
