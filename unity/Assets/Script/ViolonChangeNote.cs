using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ViolonChangeNote : MonoBehaviour
{
    public GameObject[] violonPrefabs;
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
        GameObject currentViolon = transform.Find($"violonPrefab{currentPrefabIndex}")?.gameObject;
        Vector3 oldPosition = Vector3.zero; // Position par défaut

        if (currentViolon != null)
        {
            oldPosition = currentViolon.transform.position; // Stocke la position
            Destroy(currentViolon);
        }

        // Mise à jour de l'index, en s'assurant qu'il reste dans la plage valide
        currentPrefabIndex = (currentPrefabIndex + change + violonPrefabs.Length) % violonPrefabs.Length;

        // Création du nouveau piano à l'ancienne position
        GameObject newViolon = Instantiate(violonPrefabs[currentPrefabIndex], oldPosition, Quaternion.identity, transform);
        newViolon.name = $"pianoPrefab{currentPrefabIndex}";
    }
}
