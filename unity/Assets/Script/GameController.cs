using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private bool isSoundOn = true;
    public GameObject warningSoundObject; // Le GameObject qui contient l'AudioSource
    private AudioSource warningSource; // R�f�rence interne � l'AudioSource
    public AudioClip warningClip; // Glissez votre clip audio ici dans l'inspecteur
    public GameObject messagePrefab;
    public Canvas canvas; // R�f�rence au Canvas

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

        // Assignation et v�rification de warningSoundObject et warningSource
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
            Debug.LogError("warningSoundObject n'est pas d�fini");
        }

        // Trouver et assigner le Canvas
        canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas introuvable dans la sc�ne.");
        }

        // V�rification que le prefab de message est d�fini
        if (messagePrefab == null)
        {
            Debug.LogError("messagePrefab n'est pas d�fini");
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

    // M�thode d'origine pour jouer le son d'avertissement
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

    // Cr�er un nouvel AudioSource via script
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
        if (warningSource != null && warningSource.isPlaying)
        {
            warningSource.Stop();
        }
    }

    public void CreateMessageAtCenter()
    {
        if (canvas != null && messagePrefab != null)
        {
            GameObject message = Instantiate(messagePrefab, canvas.transform);
            RectTransform rectTransform = message.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero; // Centre du canvas en utilisant anchoredPosition

            message.name = "MessagePrefab";
            // Configurez le texte du message ici si n�cessaire
        }
        else
        {
            Debug.LogError("Canvas ou messagePrefab n'est pas d�fini");
        }
    }
}
