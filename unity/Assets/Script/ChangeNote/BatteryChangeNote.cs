using UnityEngine;

public class BatteryChangeNote : MonoBehaviour
{
    public GameObject[] batteryPrefabs;
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
        GameObject currentBattery = transform.Find($"batteryPrefab{currentPrefabIndex}")?.gameObject;
        Vector3 oldPosition = Vector3.zero; // Position par défaut

        if (currentBattery != null)
        {
            oldPosition = currentBattery.transform.position; // Stocke la position
            Destroy(currentBattery);
        }

        // Mise à jour de l'index, en s'assurant qu'il reste dans la plage valide
        currentPrefabIndex = (currentPrefabIndex + change + batteryPrefabs.Length) % batteryPrefabs.Length;

        // Création du nouveau piano à l'ancienne position
        GameObject newBattery = Instantiate(batteryPrefabs[currentPrefabIndex], oldPosition, Quaternion.identity, transform);
        newBattery.name = $"pianoPrefab{currentPrefabIndex}";
    }
}
