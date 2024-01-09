using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ViolonChangeNote : MonoBehaviour
{
    public GameObject[] violonPrefabs;
    private int guitareId = 3;
    private int currentViolonIndex = -1;

    void Update()
    {
        float rotation = OSC.GetInstrumenRotation(guitareId);
        if(rotation >= 0 && rotation < 0.5 && currentViolonIndex != 0) ChangePrefab(0);
        if(rotation >= 0.5 && rotation < 1 && currentViolonIndex != 1) ChangePrefab(1);
        if(rotation >= 1 && rotation < 1.5 && currentViolonIndex != 2) ChangePrefab(2);
        if(rotation >= 1.5 && rotation < 2 && currentViolonIndex != 3) ChangePrefab(3);
        if(rotation >= 2 && rotation < 2.5 && currentViolonIndex != 4) ChangePrefab(4);
        if(rotation >= 2.5 && rotation < 3 && currentViolonIndex != 5) ChangePrefab(5);
        if(rotation >= 3 && rotation < 3.5 && currentViolonIndex != 6) ChangePrefab(6);
        if(rotation >= 3.5 && rotation < 4 && currentViolonIndex != 7) ChangePrefab(7);
        if(rotation >= 4 && rotation < 4.5 && currentViolonIndex != 8) ChangePrefab(8);
        if(rotation >= 4.5 && rotation < 5 && currentViolonIndex != 9) ChangePrefab(9);
        if(rotation >= 5 && rotation < 5.5 && currentViolonIndex != 10) ChangePrefab(10);
        if(rotation >= 5.5 && rotation < 6 && currentViolonIndex != 11) ChangePrefab(11);
        if(rotation >= 6 && rotation < 6.3 && currentViolonIndex != 11) ChangePrefab(11);
    }


    void ChangePrefab(int targetIndex)
    {
        if (currentViolonIndex == targetIndex - 1) return;
        currentViolonIndex = targetIndex - 1;
        GameObject currentViolon = ObjectFinder.FindViolonPrefab();
        Vector3 oldPosition = Vector3.zero;

        if (currentViolon != null)
        {
            oldPosition = currentViolon.transform.position;
            Destroy(currentViolon);
        }
        
        GameObject newViolon = Instantiate(violonPrefabs[targetIndex], oldPosition, Quaternion.identity, transform);
        newViolon.name = $"violonPrefab{targetIndex}";
    }
}
