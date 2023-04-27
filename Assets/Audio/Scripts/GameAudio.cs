using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip walkSounds;
    public AudioClip nightSounds;
    public AudioClip clockTick;
    public AudioClip moneyChing;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = nightSounds;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalkAudio()
    {
        audioSource.PlayOneShot(walkSounds);
    }

    public void PlayMoneyAudio()
    {
        audioSource.PlayOneShot(moneyChing);
    }

    public void PlayNightAudio()
    {
        audioSource.clip = nightSounds;
        audioSource.Play();
    }

    public void PlayClockAudio()
    {
        audioSource.clip = clockTick;
        audioSource.Play();
    }
}
