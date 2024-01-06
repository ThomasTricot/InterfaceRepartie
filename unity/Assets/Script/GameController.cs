using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isSoundOn = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSound();
        }
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
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