using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] public AudioClip fireCannon;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        switch (name) {
            case "fireCannon":
                audioSource.PlayOneShot(fireCannon);
                break;
            default:
                break;
        }
}
}
