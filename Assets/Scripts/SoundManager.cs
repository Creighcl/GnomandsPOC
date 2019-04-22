using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioClip fireCannon;


    AudioSource audioSource;
    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

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
