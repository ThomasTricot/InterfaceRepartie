using UnityEngine;

public class InstrumentSpawner : MonoBehaviour
{
    public GameObject pianoPrefab;
    public GameObject batteryPrefab;
    public GameObject guitarePrefab;
    public GameObject violonPrefab;


    public GameObject musiquePrefab;
    public GameObject validatePrefab;

    private static Canvas canvas;
    private PianoChangeNote pianoChangeNote;
    private string name;

    public GameObject reponsePrefab;

    public WebSocketClient webSocketClient;

    public static InstrumentSpawner Instance { get; private set; }

    void Awake()
    {
        // Assignation du singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        name = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            name = "reponsePrefab0"; // Nom temporaire pour l'objet "r�ponse"
            SpawnReponseAtMouse(reponsePrefab); // Utilisez la m�thode existante pour cr�er l'objet
            GameController.Instance.ResponsePlaced();
        }
        if (Input.GetKeyDown(KeyCode.M)) // Choisissez la touche pour musique
        {
            name = "musiquePrefab0";
            SpawnInstrumentAtMouse(musiquePrefab);
            GameController.Instance.MusicPlaced();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            name = "validatePrefab0";
            SpawnInstrumentAtMouse(validatePrefab);
            int tableID = 1; // Remplacez par la méthode appropriée pour obtenir l'ID de la table actuelle
            webSocketClient.SubmitFinal(tableID);
        }
    }

    public static void SpawnPianoAt(Vector2 screenPosition)
    {
        if (Instance != null)
        {
            SpawnInstrumentAt(screenPosition, Instance.pianoPrefab, "pianoPrefab0");
        }
    }

    public static void SpawnBatteryAt(Vector2 screenPosition)
    {
        if (Instance != null)
        {
            SpawnInstrumentAt(screenPosition, Instance.batteryPrefab, "batteryPrefab0");
        }
    }

    public static void SpawnGuitareAt(Vector2 screenPosition)
    {
        if (Instance != null)
        {
            SpawnInstrumentAt(screenPosition, Instance.guitarePrefab, "guitarePrefab0");
        }
    }

    public static void SpawnViolonAt(Vector2 screenPosition)
    {
        if (Instance != null)
        {
            SpawnInstrumentAt(screenPosition, Instance.violonPrefab, "violonPrefab0");
        }
    }

    public static void SpawnMusiqueAt(Vector2 screenPosition)
    {
        if (Instance != null)
        {
            SpawnInstrumentAt(screenPosition, Instance.musiquePrefab, "musiquePrefab0");
            GameController.Instance.MusicPlaced();
        }
    }

    public static void SpawnValidateAt(Vector2 screenPosition)
    {
        if (Instance != null)
        {
            SpawnInstrumentAt(screenPosition, Instance.validatePrefab, "validatePrefab0");
        }
    }

    public static void SpawnReponseAt(Vector2 screenPosition)
    {
        if (Instance != null)
        {
            SpawnInstrumentAt(screenPosition, Instance.reponsePrefab, "reponsePrefab0");
            GameController.Instance.ResponsePlaced();
        }
    }

    private static void SpawnInstrumentAt(Vector2 screenPosition, GameObject instrumentPrefab, string instrumentName)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            screenPosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        GameObject instrument = Instantiate(instrumentPrefab, canvas.transform);
        instrument.name = instrumentName;
        RectTransform rectTransform = instrument.GetComponent<RectTransform>();
        rectTransform.localPosition = localPoint;
        
        RestartAllAudioSources();
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
    
    private static void RestartAllAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
            audioSource.Play();
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