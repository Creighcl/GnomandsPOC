using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        TurretSceneManager.Instance.OnPlayerAttack += HandlePlayerAttack;
    }

    private void OnDestroy()
    {
        TurretSceneManager.Instance.OnPlayerAttack -= HandlePlayerAttack;
    }

    private void HandlePlayerAttack()
    {
        PlaySound(AudioClips.POCCannon);
    }

    public void PlaySound(AudioClips clip)
    {
        AudioClip audioClip = ResourceLoader.GetAudioClipByName(clip.ToString());
        if (audioClip != null)
        {
            audioSource?.PlayOneShot(audioClip);
        }
    }
}
