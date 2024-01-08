using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // Vitesse de rotation en degrés par seconde

    void Update()
    {
        // Faites tourner l'objet autour de son axe z à chaque frame
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
