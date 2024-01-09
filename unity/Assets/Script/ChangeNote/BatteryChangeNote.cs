using UnityEngine;
using UnityEngine.Serialization;

public class BatteryChangeNote : MonoBehaviour
{
    public GameObject[] batteryPrefabs;
    private int batteryId = 2;
    private int currentBatteryIndex = -1;

    void Update()
    {
        float rotation = OSC.GetInstrumenRotation(batteryId);
        if(rotation >= 0 && rotation < 0.5 && currentBatteryIndex != 0) ChangePrefab(0);
        if(rotation >= 0.5 && rotation < 1 && currentBatteryIndex != 1) ChangePrefab(1);
        if(rotation >= 1 && rotation < 1.5 && currentBatteryIndex != 2) ChangePrefab(2);
        if(rotation >= 1.5 && rotation < 2 && currentBatteryIndex != 3) ChangePrefab(3);
        if(rotation >= 2 && rotation < 2.5 && currentBatteryIndex != 4) ChangePrefab(4);
        if(rotation >= 2.5 && rotation < 3 && currentBatteryIndex != 5) ChangePrefab(5);
        if(rotation >= 3 && rotation < 3.5 && currentBatteryIndex != 6) ChangePrefab(6);
        if(rotation >= 3.5 && rotation < 4 && currentBatteryIndex != 7) ChangePrefab(7);
        if(rotation >= 4 && rotation < 4.5 && currentBatteryIndex != 8) ChangePrefab(8);
        if(rotation >= 4.5 && rotation < 5 && currentBatteryIndex != 9) ChangePrefab(9);
        if(rotation >= 5 && rotation < 5.5 && currentBatteryIndex != 10) ChangePrefab(10);
        if(rotation >= 5.5 && rotation < 6 && currentBatteryIndex != 11) ChangePrefab(11);
        if(rotation >= 6 && rotation < 6.3 && currentBatteryIndex != 11) ChangePrefab(11);
    }


    void ChangePrefab(int targetIndex)
    {
        if (currentBatteryIndex == targetIndex - 1) return;
        currentBatteryIndex = targetIndex - 1;
        GameObject currentBattery = ObjectFinder.FindBatteryPrefab();
        Vector3 oldPosition = Vector3.zero;

        if (currentBattery != null)
        {
            oldPosition = currentBattery.transform.position;
            Destroy(currentBattery);
        }
        
        GameObject newBattery = Instantiate(batteryPrefabs[targetIndex], oldPosition, Quaternion.identity, transform);
        newBattery.name = $"batteryPrefab{targetIndex}";
    }
}
