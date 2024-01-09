using UnityEngine;

public class PianoChangeNote : MonoBehaviour
{
    public GameObject[] pianoPrefabs;
    private int pianoId = 0;
    private int currentPianoIndex = -1;

    void Update()
    {
        float rotation = OSC.GetInstrumenRotation(pianoId);
        if(rotation >= 0 && rotation < 0.5 && currentPianoIndex != 0) ChangePrefab(0);
        if(rotation >= 0.5 && rotation < 1 && currentPianoIndex != 1) ChangePrefab(1);
        if(rotation >= 1 && rotation < 1.5 && currentPianoIndex != 2) ChangePrefab(2);
        if(rotation >= 1.5 && rotation < 2 && currentPianoIndex != 3) ChangePrefab(3);
        if(rotation >= 2 && rotation < 2.5 && currentPianoIndex != 4) ChangePrefab(4);
        if(rotation >= 2.5 && rotation < 3 && currentPianoIndex != 5) ChangePrefab(5);
        if(rotation >= 3 && rotation < 3.5 && currentPianoIndex != 6) ChangePrefab(6);
        if(rotation >= 3.5 && rotation < 4 && currentPianoIndex != 7) ChangePrefab(7);
        if(rotation >= 4 && rotation < 4.5 && currentPianoIndex != 8) ChangePrefab(8);
        if(rotation >= 4.5 && rotation < 5 && currentPianoIndex != 9) ChangePrefab(9);
        if(rotation >= 5 && rotation < 5.5 && currentPianoIndex != 10) ChangePrefab(10);
        if(rotation >= 5.5 && rotation < 6 && currentPianoIndex != 11) ChangePrefab(11);
        if(rotation >= 6 && rotation < 6.3 && currentPianoIndex != 11) ChangePrefab(11);
    }


    void ChangePrefab(int targetIndex)
    {
        if (currentPianoIndex == targetIndex - 1) return;
        currentPianoIndex = targetIndex - 1;
        GameObject currentPiano = ObjectFinder.FindPianoPrefab();
        Vector3 oldPosition = Vector3.zero;

        if (currentPiano != null)
        {
            oldPosition = currentPiano.transform.position;
            Destroy(currentPiano);
        }
        
        GameObject newPiano = Instantiate(pianoPrefabs[targetIndex], oldPosition, Quaternion.identity, transform);
        newPiano.name = $"pianoPrefab{targetIndex}";
    }
}