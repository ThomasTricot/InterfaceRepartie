using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private bool isSoundOn = true;
    public GameObject warningSoundObject; // Le GameObject qui contient l'AudioSource
    private AudioSource warningSource; // Référence interne à l'AudioSource
    public AudioClip warningClip; // Glissez votre clip audio ici dans l'inspecteur

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if (warningSoundObject != null)
        {
            warningSource = warningSoundObject.GetComponent<AudioSource>();
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
        if (warningSource != null && warningSource.isPlaying)
        {
            warningSource.Stop();
        }
    }
}
