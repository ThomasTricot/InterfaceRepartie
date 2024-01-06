using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script;


public class ViolonMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject violon;
    
    private int violonId = 3;



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
        Vector2 position = OSC.GetInstrumentPosition(violonId);
        
        violon = GameObject.Find("violonPrefab0");
        if (position != Vector2.zero && violon == null)
        {
            InstrumentSpawner.SpawnViolonAt(CoordConvertor.Convert(position[0], position[1]));
        }

        
        if (violon != null)
        {
            RectTransform violonRectTransform = violon.GetComponent<RectTransform>();
            if (violonRectTransform != null)
            {
                violonRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]);
            }
            else
            {
                Debug.LogError("The piano prefab does not have a RectTransform component.");
            }
        }
    }
}
