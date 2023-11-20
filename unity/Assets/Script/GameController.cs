using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetDataFromAPI());
        }
    }

    IEnumerator GetDataFromAPI()
    {
        string url = "http://localhost:3000/api/data/test";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Envoi de la requête et attente de la réponse
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log("Erreur : " + webRequest.error);
            }
            else
            {
                // Afficher la réponse
                Debug.Log("Réponse reçue: " + webRequest.downloadHandler.text);
            }
        }
    }
}