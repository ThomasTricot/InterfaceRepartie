using UnityEngine;

public class InstrumentSpawner : MonoBehaviour
{
    public GameObject pianoPrefab;
    public GameObject batteryPrefab;
    public GameObject guitarePrefab;
    public GameObject violonPrefab;
    private Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnInstrumentAtMouse(pianoPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnInstrumentAtMouse(batteryPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnInstrumentAtMouse(guitarePrefab);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
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

        GameObject instrument = Instantiate(instrumentPrefab, canvas.transform);
        RectTransform rectTransform = instrument.GetComponent<RectTransform>();
        rectTransform.localPosition = localPoint;
    }
}