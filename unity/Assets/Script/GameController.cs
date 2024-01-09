using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private bool isSoundOn = true;
    public GameObject messagePrefab;
    private GameObject messageInstance; // Pour garder une référence à l'instance du message créée
    public Canvas canvas; // Référence au Canvas
    public bool answerSelected = false; // Déplacé ici

    public AudioClip musiqueTableau; // Votre piste audio MP3 pour la musique du tableau
    private GameObject musicObject; // GameObject qui joue la musique du tableau
    private AudioSource musicSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Créez la source audio pour la musique du tableau
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musiqueTableau;
        musicSource.loop = true; // Réglez sur true pour jouer en boucle
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != musicSource) // Ignore seulement musicSource
            {
                if (isSoundOn)
                {
                    audioSource.UnPause();
                }
                else
                {
                    audioSource.Pause();
                }
            }
        }
    }


    // Jouer un clip directement avec AudioSource.PlayClipAtPoint(clip, position)
    public void PlayWarningSoundDirectly()
    {
        ToggleSound();
        CreateMessageAtCenter();
    }


    public void ResponsePlaced()
    {
        Debug.Log("ResponsePlaced appelé"); // Pour confirmer que la méthode est déclenchée
        DestroyMessageObject(); // Retire le message
    }

    public void CreateMessageAtCenter()
    {
        if (canvas != null && messagePrefab != null)
        {
            // Détruire le message existant si nécessaire
            if (messageInstance != null)
            {
                Destroy(messageInstance);
            }

            // Créer un nouveau message
            messageInstance = Instantiate(messagePrefab, canvas.transform);
            RectTransform rectTransform = messageInstance.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero; // Centre du canvas en utilisant anchoredPosition

            messageInstance.name = "MessagePrefab";
            // Configurez le texte du message ici si nécessaire
        }
        else
        {
            Debug.LogError("Canvas ou messagePrefab n'est pas défini");
        }
    }

    public void DestroyMessageObject()
    {
        if (messageInstance != null)
        {
            Destroy(messageInstance);
            messageInstance = null; // Réinitialiser la référence
        }
    }

    public void MusicPlaced()
    {
        ToggleSound(); // Bascule le son
        CreateAndPlayMusic();
    }

    public void CreateAndPlayMusic()
    {
        // Si une musique est déjà en cours, l'arrêter
        if (musicObject != null)
        {
            Destroy(musicObject);
        }

        // Créer un nouveau GameObject pour la source audio
        musicObject = new GameObject("MusicObject");
        musicObject.transform.position = Camera.main.transform.position; // Ou toute autre position désirée

        musicSource = musicObject.AddComponent<AudioSource>();
        musicSource.clip = musiqueTableau;
        musicSource.volume = 1; // Définissez le volume si nécessaire
        musicSource.loop = true; // Faites boucler la musique

        musicSource.Play();
    }

    public void StopAndResetMusic()
    {
        // Arrête la musique du tableau et détruit l'objet
        if (musicObject != null)
        {
            Destroy(musicObject);
        }

        ToggleSound(); // Remet les autres sons sauf la musique du tableau
    }


    public static void DestroyPiano()
    {
        GameObject piano = ObjectFinder.FindPianoPrefab();
        if(piano) Destroy(piano);
    }

    public static void DestroyGuitare()
    {
        GameObject guitare = ObjectFinder.FindGuitarePrefab();
        if(guitare) Destroy(guitare);
    }
    
    public static void DestroyBattery()
    {
        GameObject battery = ObjectFinder.FindBatteryPrefab();
        if(battery) Destroy(battery);
    }
    
    public static void DestroyViolon()
    {
        GameObject violon = ObjectFinder.FindViolonPrefab();
        if(violon) Destroy(violon);
    }
    
    public static void DestroyMusique()
    {
        GameObject musique = GameObject.Find("musiquePrefab0");
        if(musique) Destroy(musique);
    }
    
    public static void DestroyReponse()
    {
        GameObject reponse = GameObject.Find("reponsePrefab0");
        if(reponse) Destroy(reponse);
    }


}
