using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GuitareChangeNote : MonoBehaviour
{
    public GameObject[] guitarePrefabs;
    private int guitareId = 1;
    private int currentGuitareIndex = -1;

    void Update()
    {
        float rotation = OSC.GetInstrumenRotation(guitareId);
        if(rotation >= 0 && rotation < 0.5 && currentGuitareIndex != 0) ChangePrefab(0);
        if(rotation >= 0.5 && rotation < 1 && currentGuitareIndex != 1) ChangePrefab(1);
        if(rotation >= 1 && rotation < 1.5 && currentGuitareIndex != 2) ChangePrefab(2);
        if(rotation >= 1.5 && rotation < 2 && currentGuitareIndex != 3) ChangePrefab(3);
        if(rotation >= 2 && rotation < 2.5 && currentGuitareIndex != 4) ChangePrefab(4);
        if(rotation >= 2.5 && rotation < 3 && currentGuitareIndex != 5) ChangePrefab(5);
        if(rotation >= 3 && rotation < 3.5 && currentGuitareIndex != 6) ChangePrefab(6);
        if(rotation >= 3.5 && rotation < 4 && currentGuitareIndex != 7) ChangePrefab(7);
        if(rotation >= 4 && rotation < 4.5 && currentGuitareIndex != 8) ChangePrefab(8);
        if(rotation >= 4.5 && rotation < 5 && currentGuitareIndex != 9) ChangePrefab(9);
        if(rotation >= 5 && rotation < 5.5 && currentGuitareIndex != 10) ChangePrefab(10);
        if(rotation >= 5.5 && rotation < 6 && currentGuitareIndex != 11) ChangePrefab(11);
        if(rotation >= 6 && rotation < 6.3 && currentGuitareIndex != 11) ChangePrefab(11);
    }


    void ChangePrefab(int targetIndex)
    {
        if (currentGuitareIndex == targetIndex - 1) return;
        currentGuitareIndex = targetIndex - 1;
        GameObject currentGuitare = ObjectFinder.FindGuitarePrefab();
        Vector3 oldPosition = Vector3.zero;

        if (currentGuitare != null)
        {
            oldPosition = currentGuitare.transform.position;
            Destroy(currentGuitare);
        }
        
        GameObject newGuitare = Instantiate(guitarePrefabs[targetIndex], oldPosition, Quaternion.identity, transform);
        newGuitare.name = $"guitarePrefab{targetIndex}";
    }
}
