using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private bool isSoundOn = true;
    public GameObject warningSoundObject; // Le GameObject qui contient l'AudioSource
    private AudioSource warningSource; // Référence interne à l'AudioSource
    public AudioClip warningClip; // Glissez votre clip audio ici dans l'inspecteur
    public GameObject messagePrefab;
    private GameObject messageInstance; // Pour garder une référence à l'instance du message créée
    public Canvas canvas; // Référence au Canvas
    public bool answerSelected = false; // Déplacé ici

    public AudioClip musiqueTableau; // Votre piste audio MP3 pour la musique du tableau
    private GameObject musicObject;

    void Awake()
    {
        // Assignation du singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Assignation et vérification de warningSoundObject et warningSource
        if (warningSoundObject != null)
        {
            warningSource = warningSoundObject.GetComponent<AudioSource>();
            if (warningSource == null)
            {
                Debug.LogError("AudioSource introuvable sur warningSoundObject");
            }
        }
        else
        {
            Debug.LogError("warningSoundObject n'est pas défini");
        }

        // Trouver et assigner le Canvas
        canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas introuvable dans la scène.");
        }

        // Vérification que le prefab de message est défini
        if (messagePrefab == null)
        {
            Debug.LogError("messagePrefab n'est pas défini");
        }
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != warningSource) // Ignore warningSource
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

    // Méthode d'origine pour jouer le son d'avertissement
    public void PlayWarningSound()
    {
        if (!isSoundOn && warningSource != null)
        {
            warningSource.loop = true;
            warningSource.Play();
        }
    }

    // Jouer un clip directement avec AudioSource.PlayClipAtPoint(clip, position)
    public void PlayWarningSoundDirectly()
    {
        ToggleSound();
        AudioSource.PlayClipAtPoint(warningClip, Camera.main.transform.position);
        CreateMessageAtCenter();
    }

    // Créer un nouvel AudioSource via script
    public void CreateAndPlayAudioSource()
    {
        GameObject audioObject = new GameObject("TempAudio");
        audioObject.transform.position = Camera.main.transform.position;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = warningClip;
        audioSource.volume = 1;
        audioSource.loop = false;

        audioSource.Play();
        Destroy(audioObject, warningClip.length);
    }

    // Utiliser PlayOneShot(clip) pour jouer le son
    public void PlayWarningSoundOneShot()
    {
        
        if (warningSource != null && warningClip != null)
        {
            warningSource.PlayOneShot(warningClip);
        }
    }

    public void StopWarningSound()
    {
        if (warningSource != null)
        {
            warningSource.Stop();
        }
    }

    public void ResponsePlaced()
    {
        Debug.Log("ResponsePlaced appelé"); // Pour confirmer que la méthode est déclenchée
        StopWarningSound(); // Arrête la musique d'avertissement
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

        AudioSource audioSource = musicObject.AddComponent<AudioSource>();
        audioSource.clip = musiqueTableau;
        audioSource.volume = 1; // Définissez le volume si nécessaire
        audioSource.loop = true; // Faites boucler la musique

        audioSource.Play();
    }





}
