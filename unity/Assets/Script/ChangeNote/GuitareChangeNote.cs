using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GuitareChangeNote : MonoBehaviour
{
    public GameObject[] guitarePrefabs;
    private int currentPrefabIndex = 0;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     Debug.Log("droite");
        //     ChangePrefab(1); // Change au prefab suivant
        // }
        // else if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     Debug.Log("gauche");
        //     ChangePrefab(-1); // Change au prefab précédent
        // }
    }

    void ChangePrefab(int change)
    {
        GameObject currentGuitare = transform.Find($"guitarePrefab{currentPrefabIndex}")?.gameObject;
        Vector3 oldPosition = Vector3.zero; // Position par défaut

        if (currentGuitare != null)
        {
            oldPosition = currentGuitare.transform.position; // Stocke la position
            Destroy(currentGuitare);
        }

        // Mise à jour de l'index, en s'assurant qu'il reste dans la plage valide
        currentPrefabIndex = (currentPrefabIndex + change + guitarePrefabs.Length) % guitarePrefabs.Length;

        // Création du nouveau piano à l'ancienne position
        GameObject newGuitare = Instantiate(guitarePrefabs[currentPrefabIndex], oldPosition, Quaternion.identity, transform);
        newGuitare.name = $"pianoPrefab{currentPrefabIndex}";
    }
}
