using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script; // Assurez-vous que cet espace de noms contient les �l�ments n�cessaires.

public class ReponseMovement : MonoBehaviour
{
    private Canvas canvas;

    private GameObject reponse; // R�f�rence � l'objet "r�ponse"

    private int reponseId = 4; // ID pour l'objet "r�ponse"

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
        Vector2 position = OSC.GetInstrumentPosition(reponseId); // R�cup�re la position de l'objet "r�ponse"

        reponse = GameObject.Find("reponsePrefab0");
        if (position != Vector2.zero && reponse == null)
        {
            InstrumentSpawner.SpawnReponseAt(CoordConvertor.Convert(position[0], position[1])); // Cree l'objet "r�ponse" � la position sp�cifi�e
        }

        if (reponse != null)
        {
            RectTransform reponseRectTransform = reponse.GetComponent<RectTransform>();
            if (reponseRectTransform != null)
            {
                reponseRectTransform.localPosition = CoordConvertor.Convert(position[0], position[1]); // Met � jour la position de l'objet "r�ponse"
            }
            else
            {
                Debug.LogError("The reponse prefab does not have a RectTransform component.");
            }
        }
    }
}
