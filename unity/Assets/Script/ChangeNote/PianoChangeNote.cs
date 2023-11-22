using UnityEngine;

public class PianoChangeNote : MonoBehaviour
{
    public GameObject[] pianoPrefabs;
    private int currentPrefabIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangePrefab(1); // Change au prefab suivant
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangePrefab(-1); // Change au prefab précédent
        }
    }

    void ChangePrefab(int change)
    {
        // Trouve le piano actuel
        GameObject currentPiano = transform.Find($"pianoPrefab{currentPrefabIndex}")?.gameObject;
        Vector3 oldPosition = Vector3.zero; // Position par défaut

        if (currentPiano != null)
        {
            oldPosition = currentPiano.transform.position; // Stocke la position
            Destroy(currentPiano);
        }

        // Mise à jour de l'index, en s'assurant qu'il reste dans la plage valide
        currentPrefabIndex = (currentPrefabIndex + change + pianoPrefabs.Length) % pianoPrefabs.Length;

        // Création du nouveau piano à l'ancienne position
        GameObject newPiano = Instantiate(pianoPrefabs[currentPrefabIndex], oldPosition, Quaternion.identity, transform);
        newPiano.name = $"pianoPrefab{currentPrefabIndex}";
    }
}