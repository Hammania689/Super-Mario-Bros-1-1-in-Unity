using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    AudioSource sfxPlayer;

    private void Start()
    {
        sfxPlayer = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySoundFX(AudioClip sfx)
    {
        sfxPlayer.PlayOneShot(sfx);
    }
}
