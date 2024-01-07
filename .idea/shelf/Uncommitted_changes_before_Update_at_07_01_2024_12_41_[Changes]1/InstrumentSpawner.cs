using UnityEngine;

public class InstrumentSpawner : MonoBehaviour
{
    public GameObject pianoPrefab;
    public GameObject batteryPrefab;
    public GameObject guitarePrefab;
    public GameObject violonPrefab;

    public GameObject musiquePrefab;
    public GameObject validatePrefab;

    private Canvas canvas;
    private PianoChangeNote pianoChangeNote;
    private string name;

    public GameObject reponsePrefab;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            name = "reponsePrefab0"; // Nom temporaire pour l'objet "réponse"
            SpawnReponseAtMouse(reponsePrefab); // Utilisez la méthode existante pour créer l'objet
        }
        if (Input.GetKeyDown(KeyCode.M)) // Choisissez la touche pour musique
        {
            name = "musiquePrefab0";
            SpawnInstrumentAtMouse(musiquePrefab);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            name = "validatePrefab0";
            SpawnInstrumentAtMouse(validatePrefab);
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

    void SpawnReponseAtMouse(GameObject reponsePrefab)
    {
        Vector2 screenMousePos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            screenMousePos,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        GameObject reponse = Instantiate(reponsePrefab, canvas.transform);
        reponse.name = "reponsePrefab0";
        RectTransform rectTransform = reponse.GetComponent<RectTransform>();
        rectTransform.localPosition = localPoint;
    }
}