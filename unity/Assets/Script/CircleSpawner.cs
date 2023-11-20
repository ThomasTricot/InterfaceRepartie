using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab; // Assignez votre prefab de cercle dans l'inspecteur
    private Canvas canvas; // Pour référencer le Canvas

    void Start()
    {
        // Trouvez le Canvas dans la scène
        canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnCircleAtMouse();
        }
    }

    void SpawnCircleAtMouse()
    {
        // Obtenez la position de la souris sur l'écran
        Vector2 screenMousePos = Input.mousePosition;

        // Convertissez la position de l'écran en position locale du Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(), 
            screenMousePos, 
            canvas.worldCamera, 
            out Vector2 localPoint
        );

        // Instanciez le cercle
        GameObject circle = Instantiate(circlePrefab, canvas.transform);

        // Positionnez le cercle dans le Canvas
        RectTransform rectTransform = circle.GetComponent<RectTransform>();
        rectTransform.localPosition = localPoint;
    }
}