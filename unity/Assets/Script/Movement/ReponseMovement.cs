using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script; // Assurez-vous que cet espace de noms contient les éléments nécessaires.

public class ReponseMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject reponse; // Référence à l'objet "réponse"

    private int reponseId = 4; // ID pour l'objet "réponse"

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
        Vector2 position = OSC.GetInstrumentPosition(reponseId); // Récupère la position de l'objet "réponse"

        reponse = GameObject.Find("reponsePrefab0");
        if (position != Vector2.zero && reponse == null)
        {
            InstrumentSpawner.SpawnReponseAt(CoordConvertor.Convert(position[0], position[1])); // Crée l'objet "réponse" à la position spécifiée
        }

        if (reponse != null)
        {
            RectTransform reponseRectTransform = reponse.GetComponent<RectTransform>();
            if (reponseRectTransform != null)
            {
                reponseRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]); // Met à jour la position de l'objet "réponse"
            }
            else
            {
                Debug.LogError("The reponse prefab does not have a RectTransform component.");
            }
        }
    }
}
