using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // Vitesse de rotation en degr?s par seconde

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}