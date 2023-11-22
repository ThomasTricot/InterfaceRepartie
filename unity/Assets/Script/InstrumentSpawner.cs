using UnityEngine;

public class InstrumentSpawner : MonoBehaviour
{
    public GameObject pianoPrefab;
    public GameObject batteryPrefab;
    public GameObject guitarePrefab;
    public GameObject violonPrefab;
    private Canvas canvas;
    private PianoChangeNote pianoChangeNote;
    private string name;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        name = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            name = "pianoPrefab0";
            SpawnInstrumentAtMouse(pianoPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            name = "batteryPrefab0";
            SpawnInstrumentAtMouse(batteryPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            name = "guitarePrefab0";
            SpawnInstrumentAtMouse(guitarePrefab);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            name = "violonPrefab0";
            SpawnInstrumentAtMouse(violonPrefab);
        }
    }

    void SpawnInstrumentAtMouse(GameObject instrumentPrefab)
    {
        Vector2 screenMousePos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(), 
            screenMousePos, 
            canvas.worldCamera, 
            out Vector2 localPoint
        );
        

        if (name == "")
        {
            return;
        }
        
        GameObject instrument = Instantiate(instrumentPrefab, canvas.transform);
        instrument.name = name;
        RectTransform rectTransform = instrument.GetComponent<RectTransform>();
        rectTransform.localPosition = localPoint;
    }
}